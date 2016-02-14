using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportClassifier.Web.Infrastructure.Const
{
    public class Constants
    {

        public const int DEFAULT_PAGE_SIZE = 20;
        /// <summary>
        /// we find this value by experiment in a couple of attemps when the area was devided by this value, it's calculated in square meters
        /// </summary>
        public const double SQUARE_METER_DELIMATER = 1.9;
        public const int ZERO = 0;
        public const int INVALID_ID = -1;
        public static List<string> USER_UPLOAD_FILE_EXTENTIONS = new List<string>() { "shp","dbf","shx","prj"};
        public const string   CURRENT_VIEW_MODEL = "CURRENT_VIEW_MODEL";
        public const string   CURRENT_PARTIAL_VIEW_MODEL = "CURRENT_PARTIAL_VIEW_MODEL";
        public const string GEOMETRY_POLYGON_START = "POLYGON ((";

        //view data keys
       public const string   VD_KEY_USER_FIELD_CROP_TYPE = "VD_KEY_USER_FIELD_CROP_TYPE";
        public const string   VD_KEY_USER_FIELD_SOIL_TYPE = "VD_KEY_USER_FIELD_SOIL_TYPE";
        public const string   VD_KEY_USER_FIELD_DESCRIPTION = "VD_KEY_USER_FIELD_DESCRIPTION";

        //Context.Aplication keys
        public const string   APP_KEY_FOR_KEY_VALUES= "APP_KEY_FOR_KEY_VALUES";
        public const string   APP_KEY_FOR_KEY_TYPES= "APP_KEY_FOR_KEY_TYPES";
        public const string   APP_KEY_SETTINGS= "APP_KEY_SETTINGS";

    }
}