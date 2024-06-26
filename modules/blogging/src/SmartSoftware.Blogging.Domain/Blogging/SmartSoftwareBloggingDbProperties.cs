﻿using SmartSoftware.Data;

namespace SmartSoftware.Blogging
{
    public static class SmartSoftwareBloggingDbProperties
    {
        /// <summary>
        /// Default value: "Blg".
        /// </summary>
        public static string DbTablePrefix { get; set; } = "Blg";

        /// <summary>
        /// Default value: "null".
        /// </summary>
        public static string DbSchema { get; set; } = SmartSoftwareCommonDbProperties.DbSchema;

        /// <summary>
        /// "Blogging".
        /// </summary>
        public const string ConnectionStringName = "Blogging";
    }
}
