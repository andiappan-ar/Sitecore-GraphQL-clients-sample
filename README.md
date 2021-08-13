# Sitecore-GraphQL-headless-clients-sample

This project contains sample integration of Sitecore GraphQL with different clients.
The clients are implemented in headless manner. GraphQL API will bring data from sitecore to this clients.

Clients | Available features 
--- | --- 
.Net ConsoleApplication | * Authentication using sitecore credentials <br> * Set authentication cookie to GraphQL requests <br> * Sample GraphQL query implementation from sitecore instance
Xamarin - Android & IOS | * Login page implementation and authenticate using sitecore credentials <br> * Set authentication cookie to GraphQL requests <br> * Sample GraphQL query implementation from sitecore instance <br> * Sample Page navigation with Text,Image fields
## Pre-requisites
1. Setup GraphQL in your Sitecore instance.
2. Create user in Sitecore. This is used to authenticate your GraphQL calls.
3. If you are doing this excercise in your local Sitecore instance with XAMARIN. Use NGORK(https://ngrok.com/) tunnel to connect your local instance to emulator or mobile.

## Working samples
### .Net ConsoleApplication
* Login with your sitecore user name and password. Here we will login and get authentication token.
* Then run your graphQL query.

![image](https://user-images.githubusercontent.com/11770345/129423167-8f159c4e-3af9-429e-9b62-fbe6f6a79ac0.png)

### XAMARIN - Android and IOS
* Goto login page and login. Here we will login and get authentication token.
* Then goto Browse, Here you can see the list of pages under home.
* Click the individual page and see the detailed page.

Login | Browse | List of pages | Detailed view 
--- | --- | --- | --- 
![image](https://user-images.githubusercontent.com/11770345/129423655-7225efc6-0f4c-4ee3-8900-bb8fbb0ca0e4.png) | ![image](https://user-images.githubusercontent.com/11770345/129423719-418b285c-e3a7-4b93-9c74-d491919b14f5.png) | ![image](https://user-images.githubusercontent.com/11770345/129423822-f6a5dbda-6f5b-4beb-89b1-64034b6c87ed.png) | ![image](https://user-images.githubusercontent.com/11770345/129423927-1534e5fc-1efe-4175-9b8a-edb0548081a1.png)













