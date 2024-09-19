CREATE TABLE [dbo].[BasketItem]
(
	[SKU] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [Price] DECIMAL(6, 2) NOT NULL, 
    [OfferId] INT NULL
)
