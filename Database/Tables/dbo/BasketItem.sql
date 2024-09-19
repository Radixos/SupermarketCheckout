CREATE TABLE [dbo].[BasketItem]
(
	[SKU] TEXT NOT NULL PRIMARY KEY, 
    [Price] DECIMAL(6, 2) NOT NULL, 
    [OfferId] INT NULL
)
