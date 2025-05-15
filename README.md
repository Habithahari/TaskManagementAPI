Task Management API
1.Created TaskManagementAPI project in visual studio 2022 with .net core 8.0.
2.TaskManagementAPI has endpoints for Authentication and Task Management.
3.Authentication endpoint POST/api/Auth/login can authenticate the users( admin or user) and can generate the token. 
4.Default credentials are given to admin and user. 
a)Admin
i.Username: admin
ii.Password: admin123
b)User
i.Username: user
ii.Password: user123
5.Task Management end points
a)POST/api/Tasks : to add tasks. Only admin can add the tasks.
b)GET/api/Tasks/{id} : to get tasks by id.
c)GET/api/Tasks/user/{userId}: to get tasks assigned to a user.
6.Run your application and navigate to the Swagger UI.
7.Find the authentication endpoint and generate token.
8.Copy the token generated.
9.Click the "Authorize" button, In the dialog, enter: Bearer Your_token and authorize so that you can test any task endpoints.
10.Backup file of database is in folder named DBFiles.
