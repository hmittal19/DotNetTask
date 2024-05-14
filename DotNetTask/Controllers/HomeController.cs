using Azure;
using DotNetTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System;
using System.Data;
using System.Diagnostics;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DotNetTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // The Cosmos client instance
        private CosmosClient cosmosClient;

        // The database we will create
        private Microsoft.Azure.Cosmos.Database database;

        // The container we will create.
        private Microsoft.Azure.Cosmos.Container container;

        // The name of the database and container we will create
        private string databaseId = "db";
        //private string containerId = "DemoTest";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        [Route("createApplication")]
        [AllowAnonymous]
        public async Task<ActionResult> CreateApplication(CreateApplication createApplication)
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
                //DatabaseResponse databaseResourceResponse = await this.database.DeleteAsync();
                try
                {
                    this.cosmosClient = new CosmosClient(GlobalSetting.EndpointUri(), GlobalSetting.PrimaryKey(), new CosmosClientOptions() { ApplicationName = "CosmosDBDotnetQuickstart" });
                    this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
                    this.container = await this.database.CreateContainerIfNotExistsAsync("ApplicationMaster", "/ProgrameTitle", 400);
                    // Read the item to see if it exists.  
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
                    // Create an item in the container representing the Andersen family. Note we provide the value of the partition key for this item, which is "Andersen"


                    // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.

                }
                

                
            }
            catch (Exception ex)
            {
                
            }
            return Ok(response);
        }
        [HttpPost]
        [Route("createQuestion")]
        [AllowAnonymous]
        public async Task<ActionResult> CreateQuestion(QuestionsList questionsList)
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
                    // Read the item to see if it exists.  
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
                    // Create an item in the container representing the Andersen family. Note we provide the value of the partition key for this item, which is "Andersen"


                    // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.

                }
                

                
            }
            catch (Exception ex)
            {
                
            }
            return Ok(response);
        }
        [HttpPost]
        [Route("submitApplicationForm")]
        [AllowAnonymous]
        public async Task<ActionResult> SubmitApplicationForm(ApplicationForm questionsList)
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
                    // Read the item to see if it exists.  
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
                    // Create an item in the container representing the Andersen family. Note we provide the value of the partition key for this item, which is "Andersen"


                    // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.

                }
                

                
            }
            catch (Exception ex)
            {
                
            }
            return Ok(response);
        }
        [HttpGet]
        [Route("getQuestions")]
        [AllowAnonymous]
        public async Task<ActionResult> GetQuestions()
        {
            BaseDataModel response = new BaseDataModel
            {
                Status = false
            };
            try
            {
                //createApplication.Id = createApplication.ProgrameTitle + ".2";
                this.cosmosClient = new CosmosClient(GlobalSetting.EndpointUri(), GlobalSetting.PrimaryKey(), new CosmosClientOptions() { ApplicationName = "CosmosDBDotnetQuickstart" });
                this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
                this.container = await this.database.CreateContainerIfNotExistsAsync("QuestionsMaster", "/Type", 400);
                //this.container = await this.database.CreateContainerIfNotExistsAsync("ApplicationMaster", "/ProgrameTitle", 400);
                try
                {
                    // Read the item to see if it exists.  

                    var sqlQueryText = "SELECT * FROM c";


                    QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
                    FeedIterator<QuestionsList> queryResultSetIterator = this.container.GetItemQueryIterator<QuestionsList>(queryDefinition);
                    //FeedIterator<Application> queryResultSetIterator1 = this.container.GetItemQueryIterator<Application>(queryDefinition);

                    List<QuestionsList> families = new List<QuestionsList>();

                    while (queryResultSetIterator.HasMoreResults)
                    {
                        FeedResponse<QuestionsList> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                        foreach (QuestionsList family in currentResultSet)
                        {
                            families.Add(family);
                        }
                    }
                    //while (queryResultSetIterator1.HasMoreResults)
                    //{
                    //    FeedResponse<Application> currentResultSet1 = await queryResultSetIterator1.ReadNextAsync();
                    //    //foreach (QuestionsList family in currentResultSet1)
                            
                    //    //    families.Add(family);
                    //    //}
                    //}

                    response = new BaseDataModel
                    {
                        Status = true,
                        Result = families
                    };




                }
                catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    
                    // Create an item in the container representing the Andersen family. Note we provide the value of the partition key for this item, which is "Andersen"


                    // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.

                }
                

                
            }
            catch (Exception ex)
            {
                
            }
            return Ok(response);
        }
        [HttpGet]
        [Route("getForm")]
        [AllowAnonymous]
        public async Task<ActionResult> GetForm()
        {
            BaseDataModel response = new BaseDataModel
            {
                Status = false
            };
            try
            {
                //createApplication.Id = createApplication.ProgrameTitle + ".2";
                this.cosmosClient = new CosmosClient(GlobalSetting.EndpointUri(), GlobalSetting.PrimaryKey(), new CosmosClientOptions() { ApplicationName = "CosmosDBDotnetQuickstart" });
                this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
                this.container = await this.database.CreateContainerIfNotExistsAsync("ApplicationMaster", "/ProgrameTitle", 400);
                //this.container = await this.database.CreateContainerIfNotExistsAsync("ApplicationMaster", "/ProgrameTitle", 400);
                try
                {
                    // Read the item to see if it exists.  

                    var sqlQueryText = "SELECT * FROM c";


                    QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
                    FeedIterator<CreateApplication> queryResultSetIterator = this.container.GetItemQueryIterator<CreateApplication>(queryDefinition);
                    //FeedIterator<Application> queryResultSetIterator1 = this.container.GetItemQueryIterator<Application>(queryDefinition);

                    List<CreateApplication> families = new List<CreateApplication>();

                    while (queryResultSetIterator.HasMoreResults)
                    {
                        FeedResponse<CreateApplication> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                        foreach (CreateApplication family in currentResultSet)
                        {
                            families.Add(family);
                        }
                    }
                    //while (queryResultSetIterator1.HasMoreResults)
                    //{
                    //    FeedResponse<Application> currentResultSet1 = await queryResultSetIterator1.ReadNextAsync();
                    //    //foreach (QuestionsList family in currentResultSet1)
                            
                    //    //    families.Add(family);
                    //    //}
                    //}

                    response = new BaseDataModel
                    {
                        Status = true,
                        Result = families
                    };




                }
                catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    
                    // Create an item in the container representing the Andersen family. Note we provide the value of the partition key for this item, which is "Andersen"


                    // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.

                }
                

                
            }
            catch (Exception ex)
            {
                
            }
            return Ok(response);
        }
        [HttpPut]
        [Route("updateQuestion")]
        [AllowAnonymous]
        public async Task<ActionResult> UpdateQuestion(QuestionsList createApplication)
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

                
                // update registration status from false to true
                //itemBody.Questions.Where(x => x.Type == createApplication.) = true;
                //// update grade of child
                //itemBody.Children[0].Grade = 6;

                // replace the item with the updated content
                createApplicationResponse = await this.container.ReplaceItemAsync<QuestionsList>(itemBody, itemBody.Id, new PartitionKey(itemBody.Type));
                response = new BaseDataModel
                {
                    Status = true,
                    Result = createApplicationResponse
                };
                //try
                //{
                //    ItemResponse<Family> wakefieldFamilyResponse = await this.container.ReadItemAsync<Family>("Wakefield.7", new PartitionKey("Wakefield"));
                //    var itemBody = wakefieldFamilyResponse.Resource;

                //    // update registration status from false to true
                //    itemBody.IsRegistered = true;
                //    // update grade of child
                //    itemBody.Children[0].Grade = 6;

                //    // replace the item with the updated content
                //    wakefieldFamilyResponse = await this.container.ReplaceItemAsync<Family>(itemBody, itemBody.Id, new PartitionKey(itemBody.LastName));
                //    response = new BaseDataModel
                //    {
                //        Status = true,
                //        Result = createApplicationResponse
                //    };
                //}
                //catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                //{
                //    ItemResponse<CreateApplication> createApplicationResponse = await this.container.CreateItemAsync<CreateApplication>(createApplication, new PartitionKey(createApplication.ProgrameTitle));
                //    response = new BaseDataModel
                //    {
                //        Status = true,
                //        Result = createApplicationResponse
                //    };
                //    // Create an item in the container representing the Andersen family. Note we provide the value of the partition key for this item, which is "Andersen"


                //    // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.

                //}



            }
            catch (Exception ex)
            {

            }
            return Ok(response);
        }
        [HttpDelete]
        [Route("deleteQuestion")]
        [AllowAnonymous]
        public async Task<ActionResult> DeleteQuestion(string id,string type)
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
                //try
                //{
                //    ItemResponse<Family> wakefieldFamilyResponse = await this.container.ReadItemAsync<Family>("Wakefield.7", new PartitionKey("Wakefield"));
                //    var itemBody = wakefieldFamilyResponse.Resource;

                //    // update registration status from false to true
                //    itemBody.IsRegistered = true;
                //    // update grade of child
                //    itemBody.Children[0].Grade = 6;

                //    // replace the item with the updated content
                //    wakefieldFamilyResponse = await this.container.ReplaceItemAsync<Family>(itemBody, itemBody.Id, new PartitionKey(itemBody.LastName));
                //    response = new BaseDataModel
                //    {
                //        Status = true,
                //        Result = createApplicationResponse
                //    };
                //}
                //catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                //{
                //    ItemResponse<CreateApplication> createApplicationResponse = await this.container.CreateItemAsync<CreateApplication>(createApplication, new PartitionKey(createApplication.ProgrameTitle));
                //    response = new BaseDataModel
                //    {
                //        Status = true,
                //        Result = createApplicationResponse
                //    };
                //    // Create an item in the container representing the Andersen family. Note we provide the value of the partition key for this item, which is "Andersen"


                //    // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.

                //}



            }
            catch (Exception ex)
            {

            }
            return Ok(response);
        }
        public async Task<IActionResult> Index()
        {
            this.cosmosClient = new CosmosClient(GlobalSetting.EndpointUri(), GlobalSetting.PrimaryKey(), new CosmosClientOptions() { ApplicationName = "CosmosDBDotnetQuickstart" });
            this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
            this.container = await this.database.CreateContainerIfNotExistsAsync("QuestionMaster", "/Name", 400);
            User user = new User
            {
                Id = "Andersen.1",
                Name = "Andersen",
                Phone = "7014302549",
                Address = "King"
            };
            var sqlQueryText = "SELECT * FROM c WHERE c.Name = 'Andersen'";


            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<User> queryResultSetIterator = this.container.GetItemQueryIterator<User>(queryDefinition);

            List<User> families = new List<User>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<User> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (User family in currentResultSet)
                {
                    families.Add(family);
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
