### Features

 - Activate a lock * <br/>
 - Delete a lock * <br/>
 - Add admition for user or key to the lock * <br/>
 - Remove admition for user or key to the lock * <br/>
 - Collect and show opening history <br/>
 - Open the lock by key or user's access <br/>
 - Get all available locks <br/>
 - Create a physical key * <br/>
 - Change user for the physical key * <br/>
 - Change a lock for the physical key * <br/>
 - Delete the key * <br/>
 - Get all available keys * <br/> 
 - Different access by roles

 \* - Only for user with `Admin` role


### Before start-up

1 - Install [.NET 6.x.x](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) <br/>
2 - Intall [Postman](https://www.postman.com/downloads/)

### Start-up

1 - Download the source. Click on the `Code` button, then the `Download ZIP` button <br/>
2 - Open the folder, where you downloaded the source, and unzip the `SmartLock-main.zip` file <br/>
3 - Then you need to run the application. You can open and then run the app through some IDEs or you can just run it through CLI by the following command: <br/>
```
dotnet run
```

> Before running the app by CLI you need to change directory to `SmartLock-main/src`

4 - Import Postman collection. Open `Postman` and click on `Import` button, look at the picture below<br/>

<img src="https://github.com/denmaklucky/SmartLock/blob/main/files/4.png" width=331 height=435/>

Then you need to upload the file `SmartLock-main/postman/SmartLock by Denis Makarenko.postman_collection.json` <br/>

5 - Open `Collections` and find `SmartLock by Denis Makarenko`

Now you can use the application

### Use cases

|Case| Order requests|
|-|-|
|Activate lock| 0, 4, 3|
|Give access for user| 0, 7, 1, 3|
|Remove access for user|0, 9, 1, 3|
|Create a new key and give access to the user|0, 10, 1, 15|
|See opening history|0, 6|
|See all keys|0, 13|
|See all locks|0, 2|