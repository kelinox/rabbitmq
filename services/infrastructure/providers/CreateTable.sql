CREATE TABLE Email(
Id int identity(1,1) not null,
To nvarchar(40) not null,
From nvarchar(40) not null,
Body text not null);