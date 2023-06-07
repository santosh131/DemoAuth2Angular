Includes implementation of
1. Code First Model
2. Multiple Get
3. Custom Media Formatter
4. Repositiry pattern
5. OAuth2 Authentication

 * -------------------------------------------------------------------------------------------------------------------------
Refer to notes below (or) run the application and see it in Views/Home/Index.cshtml
 * -------------------------------------------------------------------------------------------------------------------------
 
/*
 * Code First Model         
 * 1. Create models (eg:FoodProductsModel.cs,ProductCategory.cs) which has [Key] attribute 
 *    (implies primary key of table) & [ForeignKey("CategoryId")] if any exists
 * 	1.1	Foreign key can be created in the following way
 *	eg: public int? CategoryId { get; set; }
 *		[ForeignKey("CategoryId")]
 *		public ProductCategory ProductCategory { get; set; })
 * 2. Create class which inherits the DbContext class in Models folder(<mark>eg:TestContext.cs</mark>
 *	2.1 In the constructor inherit the base with the connection name from web.config(<mark>eg: TestContext():base("name=DefaultConnection"))</mark>
 *	2.2 Create the public Properties of type <mark>DbSet&lt;T&gt;</mark> 
 * 3. Build the project
 * 4. In the PackageManager console 
 * 	4.1 Type command to enable
 *		eg: enable-migrations (this creates the Migrations folder)
 *	4.2 After enabling the migrations folder- a Migrations folder is created with the Configuration.cs class,
 *	in the <mark>Seed </mark> of Configuration class add the data for context class.
 *	This adds the reference data to the tables after the table is created
 *		eg:   context.ProductCategorys.AddOrUpdate(
 *		pc=>pc.CategoryId,
 *		new ProductCategory { CategoryId=1, CategoryName="Fruits" },
 *		new ProductCategory { CategoryId = 2, CategoryName = "Vegetables" },
 *		new ProductCategory { CategoryId = 3, CategoryName = "Meats" },
 *		new ProductCategory { CategoryId = 4, CategoryName = "Drinks" },
 *		new ProductCategory { CategoryId = 5, CategoryName = "Diary" }
 *		);
 *
 * 5. In the PackageManager console 
 * 	5.1 Type command to add migration
 *		eg: add-migration abc (this adds the new migration -something like restore point)
 *	5.2 Type command to update the migration to database
 *		eg: update-database  (creates/updates the tables/ adds refernce data (from the new migration))
 * -------------------------------------------------------------------------------------------------------------------------
 * Implement Multiple Get in WebAPI 
 * 1. In the controller class set the RoutePrefix attribute to the class (eg: [RoutePrefix("api/FoodProducts")])
 * 2. Add the [HttpGet] attributes to all the Get functions
 * 3. Add Route attribute to the Get functions (eg: [Route("SearchFoodProducts")])
 *     3.1 If the Get function has paramters then those paramters has to in the attribute
 *         (eg:  [Route("SearchFoodProducts/{prodName}")])
 *     3.2 Configure the App_Start/WebApiConfig.Register function  to add the custom route 
 *         (eg:  config.Routes.MapHttpRoute(name:'CustomApi'...)
 *         
 * -------------------------------------------------------------------------------------------------------------------------
 * Custom Media Formatter
 * 1. Create Formatters/FoodProductsFormatter.cs class if the output has to be formatted in a custom specific format(eg: text/custom_food_products , application/custom_food_products)
 *     1.1 In the contructor of FoodProductsFormatter add the SupportedMediaTypes(here custom_food_products) and SupportedEncodings(here UTF8Encoding,UnicodeEncoding)
 *         ,while making the ajax request the header must contain- Accept:text/custom_food_products (or) Accept:application/custom_food_products
 *         for this Formatter to work, if the request has Accept:text/json (or)  Accept:text/xml then the result will JSON/XML of FoodProductsModel
 *     1.2 Add line App_Start/WebApiConfig.Register function - config.Formatters.Add(new FoodProductsFormatter()); 
 *     1.3 When the request is made to api/FoodProducts/GetSearchFoodProducts/Apple, 
 *         the result object of type FoodProductsModel is sent to WriteToStream function in FoodProductsFormatter
 *         and output here is custom formatted as "The food product named Apple has a price of 1.00;
 *   
 * -------------------------------------------------------------------------------------------------------------------------
 * Implement Repository pattern
 * 1. Create interface for repository(eg:IFoodRepository.cs)
 * 2. Create Model & implement the repository interface (eg:FoodRepository.cs)
 * 3. Create an instance of the model in the controller and use it
 * -------------------------------------------------------------------------------------------------------------------------
 * Implement OAuth2
 * Neeeds Microsoft.Owin.Cors,  Microsoft.Owin.Host.SystemWeb
 * 
 * 1. OAuthAuthorizationServerProvider --Provider/CustomAuthProvider.cs
 *     1.1 Implement GrantResourceOwnerCredentials function
 *     1.2 Implement ValidateClientAuthentication function 
 *     1.3 Implement GrantRefreshToken function (if refresh tokens have to be used)
 * 
 * 2. IAuthenticationTokenProvider --Provider/CustomRefreshTokenProvider.cs (if refresh tokens have to be used)
 *     2.1 Implement CreateAsync
 *     2.2 Implement ReceiveAsync
 *     
 * 3. Configure the Startup.cs
 *     3.1 Configuration function -app.UseOAuthAuthorizationServer... to use these providers and set the TokenEndpointPath
 *     3.2 Add line app.UseCors(CorsOptions.AllowAll);  this allows the request to be accepted by external applications
 *     
 * 4. In the web.config
 *     4.1 In appsettings <add key="owin:AutomaticAppStartup" value="true" />
 *     4.2 Add System.Webserver add tag
 *     <httpProtocol>
 *     <customHeaders>
 *       <add name="Access-Control-Allow-Origin" value="*"/>
 *       <add name="Access-Control-Allow-Headers" value="Content-Type"/>
 *       <add name="Access-Control-Allow-Methods" value="GET, POST, PUT, DELETE, OPTIONS"/>
 *     </customHeaders>
 *    </httpProtocol>
 * 
 * 5. Add Authorize attribute to the WebApi controller class (eg:  [Authorize] for FoodProductsController class)
 * 
 * 6. In App_Start/WebApiConfig.Register function- remove line config.SuppressDefaultHostAuthentication();
 *    config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType)); 
 *    
 * -------------------------------------------------------------------------------------------------------------------------
 * Client
 * 1. To get data from FoodProductsController WebApi, we need to get the bearer/access token first otherwise we get Unauthorised message
 *      1.1 To get the access token - make a call to token end point 
 *           eg: http://localhost:64380/token
 *      1.2 This token has to called with Content-Type: application/x-www-form-urlencoded and the following data
 *          1.2.1 username      -- used to validate the client- which user
 *          1.2.2 password
 *          1.2.3 grant_type    -- Implicit Grant, Authorization,Resource Owner Client Credentials, Client Credentials
 *          1.2.4 client_id     -- used to validate which client app - WebApp(App1,SampleApp1...),Mobile APP(MobApp1,SampleMobApp2...),..
 *          1.2.5 client_secret
 *      1.3 Once the request has been made, the response is received as json data with following fields
 *          1.3.1 access_token
 *          1.3.2 token_type
 *          1.3.3 expires_in
 *          1.3.4 refresh_token (note: this is optional & created only when the IAuthenticationTokenProvider is implemented
 *      1.4 Take the received access_token,refresh_token and store in session storage in the client side
 *          1.4.1 Use this access_token and now call the action in WebApi 
 *                  eg: http://localhost:64380/api/FoodProducts/SearchFoodProducts/Apple
 *          1.4.2 The request headers should contain the following 
 *                      Authorization:'Bearer ' + access_token
 *                      Content-Type:application/xml
 *                      Accept:text/json
 *          1.4.3 The result is the json data 
 *      1.5 If the access_token is expired then the client has to get the token again using the username,password...
 *          to avoid this before the expiration of the access_token make a call to token end point 
 *          (eg: http://localhost:64380/token ) with the saved refresh_token value in session
 *          eg: the request should look like this
 *             grant_type:refresh_token
 *             client_id:App1
 *             client_secret:test
 *             refresh_token:37b60857-db99-49ee-a5f4-280e128ff5a8
 *      1.6 Once the request for refresh token has been made, the response is received as json data with following fields
 *          1.6.1 access_token(this is the new access token and has to be updated in the session)
 *          1.6.2 token_type
 *          1.6.3 expires_in
 *          1.6.4 refresh_token(this is the new refresh token and has be updated in the session)
 *          
 *                      
 */
