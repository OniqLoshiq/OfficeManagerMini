﻿namespace OMM.Services.Data.Common
{
    public static class Constants
    {
        public const string DATETIME_FORMAT = "dd-MM-yyyy";

        public const string ACTIVITY_DATETIME_FORMAT = "dd/MM/yyyy";

        public const string DATETIME_COMMENT_FORMAT = "yyyy-MM-dd HH:mm:ss";
        
        public const string DEFAULT_ROLE = "Employee";

        public const string MANAGEMENT_ROLE = "Management";

        public const string MANAGEMENT_DEPARTMENT = "Management board";

        public const string ADMIN_ROLE = "Admin";

        public const string HR_ROLE = "HR";

        public const string HR_DEPARTMENT = "HR";

        public const string ACCESS_LEVEL_CLAIM = "AccessLevel";

        public const string PICTURE_SEARCH_PREFIX = "upload/";

        public const int PICTURE_PREFIX_LENGHT = 7;

        public const string PICTURE_INACTIVE_ADDON = "e_grayscale,o_60/";

        public const string STATUS_INPROGRESS = "In Progress";

        public const string STATUS_COMPLETED = "Completed";

        public const int PROGRESS_MAX_VALUE = 100;

        public const string PROJECT_MANAGER_ROLE = "Project Manager";

        public const int MINUTES_IN_HOUR = 60;

        public const string EMPTY_STRING = "";

        public const string CHANGE_DATA_EMPTY_DATE = "-";

        public const string WORKING_TIME_SPLIT_VALUE = ":";
    }
}
