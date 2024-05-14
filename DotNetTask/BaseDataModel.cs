using Newtonsoft.Json;

namespace DotNetTask
{
    public class BaseDataModel
    {
        public bool Status { get; set; }
        public string? Msg { get; set; }
        public object? Result { get; set; }
    }
    public class CreateApplication
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }
        public string ProgrameTitle { get; set; }
        public string ProgrameDescription { get; set; }
        public Questions[]? Questions { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    public class Application
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }
        public string ProgrameTitle { get; set; }
        public string ProgrameDescription { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    public class QuestionsList
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }
        public string? Type { get; set; }
        public string? Question { get; set; }
        public string[]? Choises { get; set; }
        public bool? EnableOther { get; set; }
        public int? MaxAllowed { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    public class Questions
    {
        public string? Type { get; set; }
        public string? Question { get; set; }
        public string[]? Choises { get; set; }
        public bool? EnableOther { get; set; }
        public int? MaxAllowed { get; set; }
    }

    public class ApplicationForm
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Emai { get; set; }
        public string? Phone { get; set; }
        public string? Nationality { get; set; }
        public string? CurrentResidence { get; set; }
        public string? IDNumber { get; set; }
        public string? DOB { get; set; }
        public string? Gender { get; set; }
        public QuestionAns[]? QuestionAns { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class QuestionAns
    {
        public string? Id { get; set; }
        public string? Answers { get; set; }
    }
}
