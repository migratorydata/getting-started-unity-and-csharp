#### Unity Example Application for MigratoryData Client C# API

This repository contains the source code of the MigratoryData iOS example application.

This demo application connects to the MigratoryData server running at the address `127.0.0.1:8800`. It subscribes to the subject `/server/status` and displays the messages received for that subject. You can you the PublishButton to publish a message to the subject `/server/status`.

The status and messages received for the subject `/server/status` are displayed in the `Text` component.

To update MigratoryData C# API, you can download the latest version from [here](https://migratorydata.com/downloads/migratorydata-6/). Copy the `migratorydata-client-dotnet.dll` file to the `Assets` folder.

If you don't have a MigratoryData server installed on your machine but there is docker installed you can run the following command to start MigratoryData server, otherwise you can download and install the latest version for your os from [here](https://migratorydata.com/downloads/migratorydata-6/).

```bash
docker pull migratorydata/server:latest
docker run -d --name my_migratorydata -p 8800:8800 migratorydata/server:latest
```

You can edit the source code `Client.cs` file to connect to MigratoryData installation and subscribe to your subjects.

#### REQUIREMENTS

* MigratoryData server 6.0.16 or later
* Unity 2022.* or later
* MigratoryData C# API 6.0.9 or later

