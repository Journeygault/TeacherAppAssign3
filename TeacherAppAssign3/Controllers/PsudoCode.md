The next functionality I would add would be an update records function.

Firstly I would create  a new View similar to the delete function, It would ask for a teacher ID 
Then It would link to another to the Teacher Data controller
The code here would function similarly to add and delete, however the SQL statement would be different, It would require c# to only select elements from the HTML form that were being changed ( most likely via an @ in the sql statement).
The teacherController would require a new method to link to the database.
Note: don’t forget to sanitize the SQL inputs.
Note: some JS in the form for selecting what information you want to update would look nice, however it is ultimately optional. 
