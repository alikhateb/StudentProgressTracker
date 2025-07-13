namespace StudentProgressTracker.Core;

public static class ApiRoutes
{
    private const string BaseUrl = "/api";

    public static class Students
    {
        private const string SubBaseUrl = BaseUrl + "/students";
        public const string Add = SubBaseUrl;
        public const string List = SubBaseUrl;
        public const string Details = SubBaseUrl + "/{id}";
        public const string Update = SubBaseUrl + "/{id}";
        public const string ProgressDetails = SubBaseUrl + "/{id}/progress";
        public const string UpdateProgress = SubBaseUrl + "/{id}/progress";
    }

    public static class Courses
    {
        private const string SubBaseUrl = BaseUrl + "/courses";
        public const string Add = SubBaseUrl;
        public const string List = SubBaseUrl;
        public const string Details = SubBaseUrl + "/{id}";
        public const string Update = SubBaseUrl + "/{id}";
    }
}
