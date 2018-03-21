## Reference nuget package : 
* ```GeekLearning.Events```
* ```GeekLearning.Events.AzureStorage``` If you want to Use Azure Queue Sotrage

## What you need to implement ?
* ```Event Models``` Used to type and customise your messages 
* ```Event Handlers``` Used to execute your own code  

## What you have to do ?
* ```Configure Queues``` Put your queue configuration in your appsettings

## First Step : Queue Configuration 

``` 
"Event": {
    "Providers": {
        "Azure": {
            "Type": "AzureStorage",
            "ConnectionStringName": "Here Put the name of you azure Storage Connection string"
        },
        "AnotherAzure": {
            "Type": "AzureStorage2",
            "ConnectionString": "Here Put your azure Storage Connection string"
        },
        "Memory": {
             "Type": "InMemory"
        }
    },
    "Queues": {
      "myFisrtQueueName": {
        "ProviderName": "Azure"
      },
      "MySecondQueue": {
        "ProviderName": "Memory"
      }
    }
} 
```

### Providers 
At the moment only 2 providers are aviriables InMemory and AzureStorage  
You can configure only one of them, both or use 2 seperate configuration of the same provider type 

#### AzureStorage

For azure Storage Provider you can specify the connections string name  
If you put the connection string in the connections strings sections in your appsettings.json  
Or You can use the attribute "ConnectionString" if you want directly add the connection string

## Events Models
First, You need to implement a new EventBase Class wich inherited "GeekLearning.Events.Model.EventBase" 
```
namespace MyProject.NameSpace
{
    using GeekLearning.Events.Model;

    public abstract class MyEventBase : EventBase
    {
        //Your Properties 
    }
}
```
Then Create a specific class ```for each queues``` you are using 

```
    public class MyFisrtTypedEvent : MyEventBase
    {
        //Your custom properties 
    }

```

## Event Handlers
```
public class MyHandler : IEventHandler<MyFirstTypedEvent> {
    public async Task ExecuteAsync(MyFirstTypedEvent event){
        //Your code will be execute HERE 
    } 
}
```
Your handler must implements the "IEventHandler<YourEventType>"  
And Your Class must contains the ExecuteAsync function 

## Your WebJob/Azure Function 
```
    public class MyjobReceiver
    {
        private readonly IEventReceiver eventReceiver;

        public MyJobReceiver(IEventReceiver eventReceiver)     
        {
            this.eventReceiver = eventReceiver;
        }

        public async Task ProcessQueueMessageAsync([QueueTrigger("YourQueueNameHere")] MyFirstTypedEvent message)
        {
            this.telemetryClient.TrackEvent($"{nameof(ReportingNotificationEmailEvent)} Raised !");
            await this.eventReceiver.ReceiveAsync(message);
        }

    }
```
You'll need to Inject an IEventReceiver instance to your Class  
The ReceiveAsync methode will give your message to the correct handler 

You have to make a Class for ```each queue``` you are using 

## I'm Ready, How to use it ? 
Don't Forget to add to your container/DI framwork the event receiver, the eventFactory
And the provider you are using 

Use these functions : 
* ```AddEvent()``` //You need to give your root configuration to this function 
* ```AddInMemoryQueue()``` //If you're using the InMemory Provider
* ```AddAzureStorageQueue()``` //If you're using AzureQueueStorage Provider

You need to get an instance of IEventFactory from your DI framwork 
On this object you can use the following function in order to get the Queuer you need : 
* ```GetQueuer("YourQueueName")```

On this new object you can finaly use the function : 
* ```QueueEvent(YourMessage)```

