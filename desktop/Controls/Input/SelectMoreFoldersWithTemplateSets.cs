using desktop;
using desktop.Controls;
using desktop.Controls.Input;
using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace desktop.Controls.Input
{
    public class SelectMoreFoldersWithTemplateSets
{
    EnterOneValueWindow enterNameOfSet = null;
    ComboBoxHelper<string> cbSetsFoldersHelper = null;

    public ComboBox cbSetsFolders = new ComboBox();
    TextBlock tbSetsFolders = new TextBlock();
        public string tbSetsFoldersKey = XlfKeys.SavedFoldersSets;

    public TextBlock tbFolders = new TextBlock();
        public string tbFoldersKey = XlfKeys.Folders;
    public SelectMoreFolders txtFolders = new SelectMoreFolders();

        /// <summary>
        /// A1,A3 can be zero, then wont add to grid and create content
        /// </summary>
        /// <param name="gridCbSets"></param>
        /// <param name="rowCbSets"></param>
        /// <param name="gridSmf"></param>
        /// <param name="rowSmf"></param>
    public SelectMoreFoldersWithTemplateSets(Grid gridCbSets, int rowCbSets,Grid gridSmf, int rowSmf)
    {
            txtFolders.Name = "txtFoldersSelectMoreFoldersWithTemplateSets";

            #region CbSets
            if (gridCbSets != null)
            {
                Grid.SetRow(cbSetsFolders, rowCbSets);
                Grid.SetRow(tbSetsFolders, rowCbSets);
                Grid.SetColumn(cbSetsFolders, 1);

                tbSetsFolders.Text = sess.i18n(tbSetsFoldersKey);

                gridCbSets.Children.Add(tbSetsFolders);
                gridCbSets.Children.Add(cbSetsFolders);
            }
            #endregion

            #region Smf
            if (gridSmf != null)
            {
                Grid.SetRow(txtFolders, rowSmf);
                Grid.SetRow(tbFolders, rowSmf);
                Grid.SetColumn(txtFolders, 1);

                tbFolders.Text = sess.i18n(tbFoldersKey);

                gridSmf.Children.Add(tbFolders);
                gridSmf.Children.Add(txtFolders);
            }
        #endregion

        cbSetsFoldersHelper = new ComboBoxHelper<string>(cbSetsFolders);

        var lines = SF.GetAllElementsFile(PathSetOfFolders());
        foreach (var item in lines)
        {
            cbSetsFolders.Items.Add(item[0]);
        }

        cbSetsFoldersHelper.SelectionChanged += CbSetsFolders_SelectionChanged;
        txtFolders.SaveSetAsTemplate += this_SaveSetAsTemplate;
    }

    string PathSetOfFolders()
    {
        return AppData.ci.GetFile(AppFolders.Data, "setsOfFolders.txt");
    }

    private void EnterOneValueUC_ChangeDialogResult(bool? b)
    {
        if (BTS.GetValueOfNullable(b))
        {
                string text = enterNameOfSet.enterOneValueUC.txtEnteredText.Text;
                if (cbSetsFolders.Items.Contains(text))
                {
                    ThisApp.SetStatus(TypeOfMessage.Warning, "Set " + sess.i18n(XlfKeys.withName) + AllStrings.space + text + AllStrings.space + sess.i18n(XlfKeys.alreadyExists));
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        var line = SF.PrepareToSerialization2(CA.Join(text, txtFolders.SelectedFolders()));
                        SF.AppendToFile(PathSetOfFolders(), line);
                        cbSetsFolders.Items.Add(text);
                    }
                }
        }
    }

    private void CbSetsFolders_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        string selected = cbSetsFoldersHelper.SelectedS;
        var lines = SF.GetAllElementsFile(PathSetOfFolders());
        foreach (var item in lines)
        {
            if (item[0] == selected)
            {
                txtFolders.RemoveAllFolders();
                for (int i = 1; i < item.Count; i++)
                {
                    txtFolders.AddFolder(item[i]);
                }
            }
        }
    }

    private void this_SaveSetAsTemplate()
    {
        enterNameOfSet = new EnterOneValueWindow(sess.i18n( XlfKeys.nameOfSet));

        enterNameOfSet.enterOneValueUC.ChangeDialogResult += EnterOneValueUC_ChangeDialogResult;
        //enterNameOfSet.enterOneValueUC.Accept("Websites");
        enterNameOfSet.ShowDialog();
        //SF.PrepareToSerialization()
    }
}
}