# CompanyManagement
ASP.Net Core MVC project with CRUD operations. Forms have validation. Connected Entity Framework. DbContext with SqlServer.
</br>
</br>
<h3>Idea</h3>
We have Departments in a company and Employees.</br>
Employee works in a company and should stay in some Department.</br>
The relationship is many-to-one or many employees to one department.</br>
One Department can contain few Employees, but one Employee works in one Department.</br>
Department can be without Employee, but Employee without Department not.</br>
That's why we create firstly Department and then Employee.</br>
</br>
<h3>Controllers</h3>
Controllers provide us 3 HttpGet actions of corresponding class: Index(), AddClass() and GetClass(int id).</br>
There are 3 HttpPost actions AddClass(Class class), Update(Class class) and Delete(int id) in each Controller.</br>
All actions with post requests in result lead to Index view of corresponding class.</br>
</br>
<h3>Views</h3>
Website about employees with 4 main links on MenuTab: Employees, Add Employee, Departments, Add Department.</br>
Link "Employees" leads throught controller Employee to view Index.</br>
Link "Add Employee" leads throught controller Employee to view AddEmployee.</br>
Link "Departments" leads throught controller Department to view Index.</br>
Link "Add Department" leads throught controller Department to view AddDepartment.</br>
<h4>Employee Views</h4>
View <strong>Index</strong> shows us table with data of employees.</br>
Each employee in the table has 2 buttons: Update and Delete.</br>
Button "Update" redirect us to GetEmployee(id) view.</br>
With "Delete" button we can delete employee from repository.</br>
View <strong>GetEmployee(id)</strong> shows us form with filled inputs of employee that button was pressed.</br>
There are 2 buttons: Update and Return, on GetEmployee(id) view.</br>
Button "Update" allow us to save changes that we make in the form to repository.</br>
Button "Return" redirects us to Index view.</br>
In Index view there is also button Add Employee, that has the same link as menu in MenuTab.</br>
This link redirect us to AddEmployee view.</br>
View <strong>AddEmployee</strong> is a form with inputs according to employee fields.</br>
Form in this view will be blank with readonly id input that will shows us 0.</br>
There are 2 buttons: Create and Return, on view AddEmployee.</br>
After pressing "Create" on AddEmployee view, all form inputs will be saved to the repository.</br>
"Return" button redirect us to Index view.</br>
Both forms on AddEmployee and GetEmployee(id) views validate fields before making post request.</br>
After validation we can see below each input mistakes we made during form fill process.</br>
</br>
<h4>Department Views</h4>
View <strong>Index</strong> shows us table with data of departments</br>
Each department in the table has 2 buttons: Update and Delete.</br>
Button "Update" redirect us to GetDepartment(id) view.</br>
With "Delete" button we can delete department from repository.</br>
View <strong>GetDepartment(id)</strong> shows us form with filled inputs of department that button was pressed.</br>
There are 2 buttons: Update and Return, on GetDepartment(id) view.</br>
Button "Update" allow us to save changes that we make in the form to repository.</br>
Button "Return" redirects us to Index view.</br>
In Index view there is also button Add Department, that has the same link as menu in MenuTab.</br>
This link redirect us to AddDepartment view.</br>
View <strong>AddDepartment</strong> is a form with inputs according to department fields.</br>
Form in this view will be blank with readonly id input that will shows us 0.</br>
There are 2 buttons: Create and Return, on view AddDepartment.</br>
After pressing "Create" on AddDepartment view, all form inputs will be saved to the repository.</br>
"Return" button redirect us to Index view.</br>
Both forms on AddDepartment and GetDepartment(id) views validate fields before making post request.</br>
After validation we can see below each input mistakes we made during form fill process.</br>
</br>
<h3>Repositories</h3>
EmployeesRepository and DepartmentsRepository provides us CRUD methods: GetAll, Get, Add, Update, Delete.</br>
Repositories have connection to DbContext with Entity Framework connection to SQL Server.</br>
Local DbContext has 2 DbSet: Employee and Department.</br>
At the start of the site's work was created 2 migrations: InitialCreate and DataBaseSeeding. So SQL Servers has to be updated before website starting to work.</br>
Local DbContext on the start has nexts data seed: 6 departments and 6 employees.</br>
Requests in repositories:</br>
<ul>
<li><strong>GetAll</strong> request returns list of corresponding class. Nothing special.</li>
<li><strong>Get</strong> request returns object of corresponding class with defined id, but, if id is null or is absent in the repository method returns null.</li>
<li>In <strong>Add</strong> request, repository validate everything before putting object to DbSet.</br>
Id in this request will be placed automatically by Entity Framework.</br>
<ul>
<li> Object of <strong>Employee class</strong>.<br>
String Name are required in this request, so if it null or not respect to the length, the request will be rejected.</br>
Also, Name is validating to correspond 2 substrings: First Name, space and Last Name.</br>
Field Email is required, so if they are null the request will be rejected.</br>
If field Email is not respect to the email regex request will be rejected.</br>
String Phone number must not be null or empty and has contain 12 digits, otherwise request will be rejected.</br>
Field DepartmentId is comparing with id in department repository.</li>
<li> Object of <strong>Department class</strong>.<br>
Field Name is required so if it is null or empty the request will be rejected.</br>
Also, Name of department should respect to the length of the string.</br>
If field Manager is is null or empty request will be rejected.</br>
String Manager is validating to correspond 2 substrings: First Name, space and Last Name.</br></li>
</ul>
</li>
<li>In <strong>Update</strong> request, repository validate everything before object will be passed to DbSet</br>
Id in this request will be checked if it already in the DbSet of corresponding class. If no - the request will be rejected.</br>
Other fields are validating according to Add request.</br></li>
<li><strong>Delete</strong> request passed id of object of corresponding class that has been deleted.</br>
But, if id is null or is absent in the DbContext the request will be rejected.</li>
</ul>
