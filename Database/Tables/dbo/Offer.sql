CREATE TABLE [dbo].[Offer]
(
	[OfferId] INT NOT NULL PRIMARY KEY, 
    [OfferType] NVARCHAR(50) NOT NULL, 
    [OfferQuantity] INT NULL, 
    [OfferPrice] DECIMAL(6, 2) NULL
)
