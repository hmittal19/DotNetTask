namespace DotNetTask
{
    public interface IHomeRepo
    {
        Task<BaseDataModel> CreateApplication(CreateApplication createApplication);
        Task<BaseDataModel> GetQuestions();
        Task<BaseDataModel> GetForm();
        Task<BaseDataModel> CreateQuestion(QuestionsList questionsList);
        Task<BaseDataModel> SubmitApplicationForm(ApplicationForm questionsList);
        Task<BaseDataModel> UpdateQuestion(QuestionsList questionsList);
        Task<BaseDataModel> DeleteQuestion(string id, string type);
    }
}
