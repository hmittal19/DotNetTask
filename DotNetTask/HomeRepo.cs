using Microsoft.Azure.Cosmos;
using System.Net;

namespace DotNetTask
{
    public class HomeRepo : IHomeRepo
    {
        private CosmosClient? cosmosClient;

        private Microsoft.Azure.Cosmos.Database? database;

        private Microsoft.Azure.Cosmos.Container? container;

        private string databaseId = "db";
        public async Task<BaseDataModel> CreateApplication(CreateApplication createApplication)
        {
            BaseDataModel response = new BaseDataModel
            {
                Status = false
            };
            try
            {


                this.cosmosClient = new CosmosClient(GlobalSetting.EndpointUri(), GlobalSetting.PrimaryKey(), new CosmosClientOptions() { ApplicationName = "CosmosDBDotnetQuickstart" });
                this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
                this.container = await this.database.CreateContainerIfNotExistsAsync("ApplicationMaster", "/ProgrameTitle", 400);
                Container container = this.database.GetContainer("ApplicationMaster");
                ContainerResponse response1 = await container.DeleteContainerAsync();
                try
                {
                    this.cosmosClient = new CosmosClient(GlobalSetting.EndpointUri(), GlobalSetting.PrimaryKey(), new CosmosClientOptions() { ApplicationName = "CosmosDBDotnetQuickstart" });
                    this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
                    this.container = await this.database.CreateContainerIfNotExistsAsync("ApplicationMaster", "/ProgrameTitle", 400);
                    createApplication.Id = Guid.NewGuid().ToString();
                    ItemResponse<CreateApplication> createApplicationResponse = await this.container.CreateItemAsync<CreateApplication>(createApplication, new PartitionKey(createApplication.ProgrameTitle));
                    response = new BaseDataModel
                    {
                        Status = true,
                        Result = createApplicationResponse
                    };
                }
                catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    ItemResponse<CreateApplication> createApplicationResponse = await this.container.CreateItemAsync<CreateApplication>(createApplication, new PartitionKey(createApplication.ProgrameTitle));
                    response = new BaseDataModel
                    {
                        Status = true,
                        Result = createApplicationResponse
                    };
                }
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<BaseDataModel> CreateQuestion(QuestionsList questionsList)
        {
            BaseDataModel response = new BaseDataModel
            {
                Status = false
            };
            try
            {
                try
                {
                    this.cosmosClient = new CosmosClient(GlobalSetting.EndpointUri(), GlobalSetting.PrimaryKey(), new CosmosClientOptions() { ApplicationName = "CosmosDBDotnetQuickstart" });
                    this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
                    this.container = await this.database.CreateContainerIfNotExistsAsync("QuestionsMaster", "/Type", 400);
                    questionsList.Id = Guid.NewGuid().ToString();
                    ItemResponse<QuestionsList> createApplicationResponse = await this.container.CreateItemAsync<QuestionsList>(questionsList, new PartitionKey(questionsList.Type));
                    response = new BaseDataModel
                    {
                        Status = true,
                        Result = createApplicationResponse
                    };
                }
                catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    ItemResponse<QuestionsList> createApplicationResponse = await this.container.CreateItemAsync<QuestionsList>(questionsList, new PartitionKey(questionsList.Type));
                    response = new BaseDataModel
                    {
                        Status = true,
                        Result = createApplicationResponse
                    };
                }
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<BaseDataModel> DeleteQuestion(string id, string type)
        {
            BaseDataModel response = new BaseDataModel
            {
                Status = false
            };
            try
            {

                this.cosmosClient = new CosmosClient(GlobalSetting.EndpointUri(), GlobalSetting.PrimaryKey(), new CosmosClientOptions() { ApplicationName = "CosmosDBDotnetQuickstart" });
                this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
                this.container = await this.database.CreateContainerIfNotExistsAsync("QuestionsMaster", "/Type", 400);
                ItemResponse<QuestionsList> createApplicationResponse = await this.container.DeleteItemAsync<QuestionsList>(id, new PartitionKey(type));

                response = new BaseDataModel
                {
                    Status = true,
                    Result = createApplicationResponse
                };
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<BaseDataModel> GetForm()
        {
            BaseDataModel response = new BaseDataModel
            {
                Status = false
            };
            try
            {
                this.cosmosClient = new CosmosClient(GlobalSetting.EndpointUri(), GlobalSetting.PrimaryKey(), new CosmosClientOptions() { ApplicationName = "CosmosDBDotnetQuickstart" });
                this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
                this.container = await this.database.CreateContainerIfNotExistsAsync("ApplicationMaster", "/ProgrameTitle", 400);
                try
                {
                    var sqlQueryText = "SELECT * FROM c";
                    QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
                    FeedIterator<CreateApplication> queryResultSetIterator = this.container.GetItemQueryIterator<CreateApplication>(queryDefinition);
                    List<CreateApplication> families = new List<CreateApplication>();
                    while (queryResultSetIterator.HasMoreResults)
                    {
                        FeedResponse<CreateApplication> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                        foreach (CreateApplication family in currentResultSet)
                        {
                            families.Add(family);
                        }
                    }
                    response = new BaseDataModel
                    {
                        Status = true,
                        Result = families
                    };
                }
                catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                {
                }
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<BaseDataModel> GetQuestions()
        {
            BaseDataModel response = new BaseDataModel
            {
                Status = false
            };
            try
            {
                this.cosmosClient = new CosmosClient(GlobalSetting.EndpointUri(), GlobalSetting.PrimaryKey(), new CosmosClientOptions() { ApplicationName = "CosmosDBDotnetQuickstart" });
                this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
                this.container = await this.database.CreateContainerIfNotExistsAsync("QuestionsMaster", "/Type", 400);
                try
                {
                    var sqlQueryText = "SELECT * FROM c";
                    QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
                    FeedIterator<QuestionsList> queryResultSetIterator = this.container.GetItemQueryIterator<QuestionsList>(queryDefinition);

                    List<QuestionsList> families = new List<QuestionsList>();

                    while (queryResultSetIterator.HasMoreResults)
                    {
                        FeedResponse<QuestionsList> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                        foreach (QuestionsList family in currentResultSet)
                        {
                            families.Add(family);
                        }
                    }response = new BaseDataModel
                    {
                        Status = true,
                        Result = families
                    };
                }
                catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                {
                }
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<BaseDataModel> SubmitApplicationForm(ApplicationForm questionsList)
        {
            BaseDataModel response = new BaseDataModel
            {
                Status = false
            };
            try
            {
                try
                {
                    this.cosmosClient = new CosmosClient(GlobalSetting.EndpointUri(), GlobalSetting.PrimaryKey(), new CosmosClientOptions() { ApplicationName = "CosmosDBDotnetQuickstart" });
                    this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
                    this.container = await this.database.CreateContainerIfNotExistsAsync("ApplicationForm", "/FirstName", 400);
                    questionsList.Id = Guid.NewGuid().ToString();
                    ItemResponse<ApplicationForm> createApplicationResponse = await this.container.CreateItemAsync<ApplicationForm>(questionsList, new PartitionKey(questionsList.FirstName));
                    response = new BaseDataModel
                    {
                        Status = true,
                        Result = createApplicationResponse
                    };
                }
                catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    ItemResponse<ApplicationForm> createApplicationResponse = await this.container.CreateItemAsync<ApplicationForm>(questionsList, new PartitionKey(questionsList.FirstName));
                    response = new BaseDataModel
                    {
                        Status = true,
                        Result = createApplicationResponse
                    };
                }
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<BaseDataModel> UpdateQuestion(QuestionsList createApplication)
        {
            BaseDataModel response = new BaseDataModel
            {
                Status = false
            };
            try
            {

                this.cosmosClient = new CosmosClient(GlobalSetting.EndpointUri(), GlobalSetting.PrimaryKey(), new CosmosClientOptions() { ApplicationName = "CosmosDBDotnetQuickstart" });
                this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
                this.container = await this.database.CreateContainerIfNotExistsAsync("QuestionsMaster", "/Type", 400);
                ItemResponse<QuestionsList> createApplicationResponse = await this.container.ReadItemAsync<QuestionsList>(createApplication.Id, new PartitionKey(createApplication.Type));
                var itemBody = createApplicationResponse.Resource;
                itemBody.Choises = createApplication.Choises;
                itemBody.Question = createApplication.Question;
                itemBody.MaxAllowed = createApplication.MaxAllowed;
                itemBody.EnableOther = createApplication.EnableOther;
                itemBody.Type = createApplication.Type;
                createApplicationResponse = await this.container.ReplaceItemAsync<QuestionsList>(itemBody, itemBody.Id, new PartitionKey(itemBody.Type));
                response = new BaseDataModel
                {
                    Status = true,
                    Result = createApplicationResponse
                };
            }
            catch (Exception ex)
            {

            }
            return response;
        }
    }
}
