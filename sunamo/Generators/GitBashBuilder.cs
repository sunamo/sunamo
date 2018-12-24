using sunamo.Constants;
using sunamo.Essential;
using sunamo.Generators.Text;
using sunamo.Values;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace sunamo.Generators
{
    /// <summary>
    /// 
    /// </summary>
    public class GitBashBuilder
    {
        public TextBuilder sb = new TextBuilder();

        public GitBashBuilder()
        {
            sb.prependEveryNoWhite = AllStrings.space;
        }

         void Git(string remainCommand)
        {
            Git(sb.sb, remainCommand);
        }

        public List<string> Commands { get => SH.GetLines(ToString()); }

        /// <summary>
        /// A2 must be files prepared to cmd
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="linesFiles"></param>
        /// <returns></returns>
        public static string CreateGitAddForFiles(StringBuilder sb, List<string> linesFiles)
        {
            return CreateGitCommandForFiles("add", sb, linesFiles);
        }

        /// <summary>
        /// Support:
        /// {dir}/* for add all files
        /// */{filename} - add files from all dirs
        /// automatically add .cs extension where is not
        /// 
        /// 
        /// A2 - full path or name in Projects folder
        /// A3 - with or without full path, without extension, can be slash and backslash
        /// </summary>
        /// <param name="tlb"></param>
        /// <param name="solution"></param>
        /// <param name="linesFiles"></param>
        /// <param name="searchOnlyWithExtension"></param>
        /// <returns></returns>
        public static string GenerateCommandForGit(TypedLoggerBase tlb, string solution, List<string> linesFiles, out bool anyError, string searchOnlyWithExtension, string command)
        {
            var filesToCommit = GitBashBuilder.PrepareFilesToSimpleGitFormat(tlb, solution, linesFiles, out anyError, searchOnlyWithExtension);
            if (filesToCommit == null)
            {
                return "";
            }

            string result = GitBashBuilder.CreateGitCommandForFiles(command, new StringBuilder(), filesToCommit);
            ClipboardHelper.SetText(result);
            return result;
        }

        /// <summary>
        /// A2 - path in which search for files by extension
        /// A4 - must be filled, because is stripped all extension then passed will be suffixed
        /// </summary>
        /// <param name="tlb"></param>
        /// <param name="solution"></param>
        /// <param name="linesFiles"></param>
        /// <param name="searchOnlyWithExtension"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static List<string> PrepareFilesToSimpleGitFormat(TypedLoggerBase tlb, string solution, List<string> linesFiles, out bool anyError, string searchOnlyWithExtension)
        {
            anyError = false;
            // removing notes and description
            //TypedLoggerBase tlb = TypedConsoleLogger.Instance;

            string pathSearchForFiles = null;
            if (Directory.Exists(solution))
            {
                pathSearchForFiles = solution;
            }
            else
            {
                pathSearchForFiles = FS.Combine(SourceCodePaths.CsProjects, solution);
            }

            string pathRepository = pathSearchForFiles;
            if (solution == Consts.scz)
            {
                tlb.Information("Is sunamo.cz");
                pathSearchForFiles += AllStrings.bs + solution;
            }
            tlb.Information("Path: " + pathSearchForFiles);

            FS.WithEndSlash(ref pathRepository);

            var files = FS.GetFiles(pathSearchForFiles, FS.MascFromExtension(), System.IO.SearchOption.AllDirectories);

            CA.Replace(linesFiles, solution, string.Empty);
            CA.ChangeContent(linesFiles, SH.RemoveAfterFirst, AllStrings.swd);
            CA.Trim(linesFiles);
            CA.ChangeContent(linesFiles, FS.AddExtensionIfDontHave, searchOnlyWithExtension);
            CA.ChangeContent<bool>(linesFiles, FS.Slash, true);
            CA.ChangeContent(linesFiles, SH.TrimStart, AllStrings.slash);
            var linesFilesOnlyFilename = FS.OnlyNames(linesFiles);

            anyError = false;
            List<string> filesToCommit = new List<string>();

            // In key are filenames, in value full paths to files
            Dictionary<string, List<string>> dictPsychicallyExistsFiles = FS.GetDictionaryByFileNameWithExtension(files);

            CA.Replace(files, AllStrings.bs, AllStrings.slash);
            pathRepository = FS.Slash(pathRepository, false);

            // process full path files
            for (int i = 0; i < linesFiles.Length(); i++)
            {
                var item = linesFilesOnlyFilename[i];
                // full path with backslash on end
                var itemWithoutTrim = linesFiles[i];
                #region Directory\*
                if (item[item.Length - 1] == AllChars.asterisk)
                {
                    item = itemWithoutTrim.TrimEnd(AllChars.asterisk);
                    string itemWithoutTrimBackslashed = FS.Combine(pathRepository, FS.Slash(item, false));
                    if (FS.ExistsDirectory(itemWithoutTrimBackslashed))
                    {
                        filesToCommit.Add(item + AllStrings.asterisk);
                    }
                    else
                    {
                        anyError = true;
                        tlb.Error(Exceptions.DirectoryWasntFound(null, itemWithoutTrimBackslashed));
                    }
                }
                #endregion
                #region *File - add all files without specify root directory
                else if (item[0] == AllChars.asterisk)
                {
                    string file = item.Substring(1);
                    foreach (var item2 in dictPsychicallyExistsFiles[file])
                    {
                        filesToCommit.Add(FS.Slash(item2.Replace(pathRepository, string.Empty), true));
                    }
                }
                #endregion
                #region Exactly defined file
                else
                {
                    #region File isnt in dict => Dont exists
                    if (!dictPsychicallyExistsFiles.ContainsKey(item))
                    {
                        anyError = true;
                        tlb.Error(Exceptions.FileWasntFoundInDirectory(null, pathSearchForFiles, item));
                    }
                    #endregion
                    else
                    {
                        string itemWithoutTrimBackslashed = FS.Combine(pathRepository, FS.Slash(itemWithoutTrim, false));
                        #region Add as relative file
                        if (itemWithoutTrim.Contains(AllStrings.slash))
                        {
                            if (File.Exists(itemWithoutTrimBackslashed))
                            {
                                filesToCommit.Add(itemWithoutTrim.Replace(pathRepository, string.Empty));
                            }
                            else
                            {
                                anyError = true;
                                tlb.Error(Exceptions.FileWasntFoundInDirectory(null, itemWithoutTrimBackslashed));
                            }
                        }
                        #endregion
                        #region Add file in root of repository
                        else
                        {
                            if (dictPsychicallyExistsFiles[item].Count == 1)
                            {
                                filesToCommit.Add(FS.Slash(dictPsychicallyExistsFiles[item][0].Replace(pathRepository, string.Empty), true));
                            }
                            else
                            {
                                anyError = true;
                                tlb.Error(Exceptions.MoreCandidates(null, dictPsychicallyExistsFiles[item], item));
                            }
                        }
                        #endregion
                    }
                }
                #endregion
            }

            if (anyError)
            {
                tlb.Error(Messages.SomeErrorsOccured);
                return null;
            }

            return filesToCommit;
        }

        public static string CreateGitCommandForFiles(string command, StringBuilder sb, List<string> linesFiles)
        {
            return Git(sb, command + " " + SH.Join(AllChars.space, linesFiles));
        }

        public void Cd(string key)
        {
            sb.AppendLine("cd " + SH.WrapWith( key, AllChars.qm)); 
        }

        public void Clear()
        {
            sb.Clear();
        }

        public void Append(string text)
        {
            sb.Append(text);
        }

        public void AppendLine(string text)
        {
            sb.AppendLine(text);
        }

        public void AppendLine()
        {
            sb.AppendLine();
        }

        public override string ToString()
        {
            return sb.ToString();
        }

        #region Git commands
        public void Clone(string repoUri)
        {
            Git("clone " + repoUri);
            AppendLine();
        }

        public void Commit(bool addAllUntrackedFiles, string commitMessage)
        {
            Git("commit ");
            if (addAllUntrackedFiles)
            {
                Append("-a");
            }
            if (!string.IsNullOrWhiteSpace(commitMessage))
            {
                Append("-m " + SH.WrapWithQm(commitMessage));
            }
            AppendLine();

        }

        public void Push(bool force)
        {
            Git("push");
            if (force)
            {
                Append("-f");
            }
            AppendLine();
        }

        public void Push(string arg)
        {
            Git("push");
            AppendLine(arg);
            AppendLine();
        }

        /// <summary>
        /// myslim si ze chyba spise ne z v initu byla v clone, init se musi udelat i kdyz chci udelat git remote
        /// nikdy nepoustet na adresar ktery ma jiz adresar .git!! jinak se mi zapise s prazdnym obsahem a pri pristim pushi mam po vsem!!! soubory mi odstrani z disku a ne do zadneho kose!!!
        /// </summary>
        public void Init()
        {
            Git("init");
            AppendLine();
        }

        public void Add(string v)
        {
            Git("add");
            Append(v);
            AppendLine();
        }

        /// <summary>
        /// never use, special with dfx argument
        /// d - Remove untracked directories in addition to untracked files.
        /// f - delete all files although conf variable clean.requireForce
        /// x - ignore rules from all .gitignore
        /// 
        /// A1 - arguments without dash
        /// </summary>
        /// <param name="v"></param>
        public void Clean(string v)
        {
            Git("clean");
            Arg(v);
            AppendLine();
        }

        /// <summary>
        /// Not automatically append new line - due to conditionals adding arguments
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="remainCommand"></param>
        /// <returns></returns>
        static string Git(StringBuilder sb, string remainCommand)
        {
            sb.Append("git " + remainCommand);
            return sb.ToString();
        } 
        #endregion


        private void Arg(string v)
        {
            Append("-" + v);
        }

        public void Remote(string arg)
        {
            Git("remote");
            Append(arg);
            AppendLine();
        }

        public void Status()
        {
            Git("status");
            AppendLine();
        }
    }
}
