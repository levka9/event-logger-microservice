# Events logger microservice using .Net Core and Nancy Framework

This app using:
* .Net Core
* Nancy Framework
* Entity Framework
* MSSQL

## Service

1. Get - return specify event by event id.

Example:
https://localhost:44365/event/2

2. Get - return last 50 events by user id.

Example:
https://localhost:44365/event/getByUserId/12

3. Post - add new event and list of event dynamic parameters. 

Example:
https://localhost:44365/event/add

Body:
{
	"EventTypeId": 2,
	"AppicationName": "test21",
	"UserId": 14,
	"ProccessId": 1111,
	"EventDetails": [
		{
		"TagName": "param1",
		"Value": "2222"
		},
		{
		"TagName": "param2",
		"Value": "3333"
		}
	]
}

## Database

This events logger use local MsSql database that located in ...\MicroServices\Microservice-EventLogger\EventLogger\LocalDB