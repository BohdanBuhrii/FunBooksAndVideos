-- Define database tables

CREATE TABLE Customers (
  Id INT PRIMARY KEY IDENTITY(1,1),
  Email NVARCHAR(255) NOT NULL
);

CREATE TABLE Memberships (
  Id INT PRIMARY KEY IDENTITY(1,1),
  ProductId INT NOT NULL,
  [Description] NVARCHAR(255),
  DurationInDays INT NOT NULL
);

CREATE TABLE CustomerMemberships (
  CustomerId INT NOT NULL,
  MembershipId INT NOT NULL,
  ExpirationDate DATETIMEOFFSET NOT NULL,
  PRIMARY KEY (CustomerId, MembershipId)
);

CREATE TABLE Products (
  Id INT PRIMARY KEY IDENTITY(1,1),
  [Description] NVARCHAR(MAX) NULL,
  [Type] SMALLINT NOT NULL,
  Price MONEY NOT NULL,
  IsPhysical BIT NOT NULL,
  CoverUrl NVARCHAR(2048) NULL,
  Display BIT NOT NULL DEFAULT 1
);

CREATE TABLE Orders (
  Id INT PRIMARY KEY IDENTITY(1,1),
  TotalAmount MONEY NOT NULL,
  CustomerId INT NOT NULL
);

CREATE TABLE OrderProducts (
  OrderId INT NOT NULL,
  ProductId INT NOT NULL,
  PRIMARY KEY (OrderId, ProductId)
);


-- Insert Customers
SET IDENTITY_INSERT Customers ON;

INSERT INTO Customers (Id, Email)
VALUES
  (1, 'customer1@example.com'),
  (2, 'customer2@example.com');

SET IDENTITY_INSERT Customers OFF;

-- Insert Memberships
SET IDENTITY_INSERT Memberships ON;

INSERT INTO Memberships (Id, ProductId, [Description], DurationInDays)
VALUES
  (1, 11, 'Book Club', 30),
  (2, 12, 'Movie Club', 30),
  (3, 13, 'Premium', 30);

SET IDENTITY_INSERT Memberships OFF;

-- Insert Products
SET IDENTITY_INSERT Products ON;

INSERT INTO Products (Id, Description, Type, Price, IsPhysical, CoverUrl)
VALUES
-- Books (Type = 2)
  (1, 'The Great Gatsby', 2, 19.99, 1, 'https://example.com/gatsby.jpg'),
  (2, '1984', 2, 14.99, 1, 'https://example.com/1984.jpg'),
  (3, 'To Kill a Mockingbird', 2, 17.50, 1, 'https://example.com/mockingbird.jpg'),
  (4, 'The Catcher in the Rye', 2, 13.99, 1, 'https://example.com/catcher.jpg'),
  (5, 'Brave New World', 2, 18.00, 1, 'https://example.com/brave.jpg'),
-- Movies (Type = 3)
  (6, 'The Matrix', 3, 9.99, 0, 'https://example.com/matrix.jpg'),
  (7, 'Inception', 3, 12.99, 0, 'https://example.com/inception.jpg'),
  (8, 'Interstellar', 3, 15.99, 0, 'https://example.com/interstellar.jpg'),
  (9, 'Pulp Fiction', 3, 10.50, 0, 'https://example.com/pulp.jpg'),
  (10, 'The Godfather', 3, 11.99, 0, 'https://example.com/godfather.jpg'),
-- Memberships (Type = 1)
  (11, 'Book Club Membership', 1, 5.99, 0, 'https://example.com/membership.jpg'),
  (12, 'Movie Club Membership', 1, 5.99, 0, 'https://example.com/membership.jpg'),
  (13, 'Premium Membership', 1, 9.99, 0, 'https://example.com/membership.jpg');

SET IDENTITY_INSERT Products OFF;


-- Add constraints after data insertion

ALTER TABLE CustomerMemberships
ADD CONSTRAINT FK_CustomerMemberships_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id),
    CONSTRAINT FK_CustomerMemberships_Memberships FOREIGN KEY (MembershipId) REFERENCES Memberships(Id);

ALTER TABLE Orders
ADD CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id);

ALTER TABLE OrderProducts
ADD CONSTRAINT FK_OrderProducts_Orders FOREIGN KEY (OrderId) REFERENCES Orders(Id),
    CONSTRAINT FK_OrderProducts_Products FOREIGN KEY (ProductId) REFERENCES Products(Id);

ALTER TABLE Memberships
ADD CONSTRAINT FK_Memberships_Products FOREIGN KEY (ProductId) REFERENCES Products(Id);