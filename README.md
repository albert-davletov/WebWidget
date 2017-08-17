# WebWidget
The base version of REST api using Web API 2.

CRUD operations (using cURL utility):<br />
Create:<br />
curl -d "{'Name':'New Widget', 'Description':'Testing', 'Price':'4'}" -H "Content-Type: application/json" -X POST http://localhost:52412/api/Widgets

Read:<br />
curl -X GET http://localhost:52412/api/Widgets/3

List:<br />
curl -X GET http://localhost:52412/api/Widgets

Update:<br />
curl -d "{'Name':'Updated Widget', 'Description':'Testing', 'Price':'555'}" -H "Content-Type: application/json" -X PUT http://localhost:52412/api/Widgets/1

Delete:<br />
curl -X DELETE http://localhost:52412/api/Widgets/2
