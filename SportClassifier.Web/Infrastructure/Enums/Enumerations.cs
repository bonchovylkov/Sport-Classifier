using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportClassifier.Web.Infrastructure.Enums
{
    public static class Enumerations
    {
        public enum KeyValueIntCode
        {
            PathUsersCropFields,
            PathAppFiles,

        }

        public enum KeyTypeIntCode
        {
            SystemFilePath,
            TypeSoils,
            FieldNoteActionType
        }

        public enum ToastrType
        {
            success,
            info,
            warning,
            error

        }

        public enum CrudActionType
        {
            Create,
            Update,
            Delete

        }


        public enum AppSettingsClass
        {
            Integer,
            String,
            Double,
            Date,
            List,
            Bolean,
        }

          public enum AppSettings
        {
            CurrentYear,
            SendEmailAfterRegister,
            PasswordMinLength,
            PageSize,
            FrontPageArticlesCount,
            //Sci hub downloader - start
            CloudCoverageMax,
            MaxQueueLength,
            SaveLocationPrefix,
            SaveLocationSuffix,
            DataHubLoginUrl,
            DataHubSearchUrl,
            Authorization,
            TwikisId,
            DhusAuthLogin,
            DhusIntegrityLogin,
            JSessionIdLogin,
            UsernamePassword,
            SearchPrefix,
            SearchSuffix,
            ItemsPerPageSearch,
            DatePassedSearch,
            StartIndexSearch,
        }


    }
}