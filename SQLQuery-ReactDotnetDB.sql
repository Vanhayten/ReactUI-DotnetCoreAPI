Create database ReactDotnetDB
GO
use ReactDotnetDB
Create database ReactDotnetDB
GO
use ReactDotnetDB
GO
create table dbo.Department(
DepartmentId int identity(1,1),
DepartmentName nvarchar(255)
)
GO
create table dbo.Employee(
EmployeeId int identity(1,1),
EmployeeName nvarchar(255),
Department nvarchar(255),
DateOfJoining datetime,
PhotoFileName nvarchar(255)
)


insert into  dbo.Department values( 'IT' )
insert into  dbo.Department values( 'Support' )

insert into  dbo.Employee values( 'Bob', 'IT', GETDATE(), 'anonymous.png' )







