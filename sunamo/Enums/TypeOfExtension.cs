using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Enums
{
    public enum TypeOfExtension
    {
        archive,
        image,
        source_code,
        document,
        database,
        /// <summary>
        /// XML, JSON, mdf, ldf, sdf, atd.
        /// </summary>
        data,
        /// <summary>
        /// ini, dat, atd.
        /// </summary>
        settings,
        visual_studio,
        executable,
        binary,
        resource,
        /// <summary>
        /// sql, cmd, ps1, 
        /// </summary>
        script,
        font,
        multimedia,
        temporary,
        /// <summary>
        /// Is used when extension isn't know
        /// U ostatních souborů vypsat jejich popis z windows
        /// </summary>
        other
    }
}
