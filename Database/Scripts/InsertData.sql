﻿INSERT INTO dbo.Product (SKU, Price, OfferId)
VALUES
('A', 50, 1),
('B', 30, 2),
('C', 20, NULL),
('D', 15, NULL);

INSERT INTO dbo.Offer (OfferId, OfferType, OfferQuantity, OfferPrice)
VALUES
(1, 'MultiBuy', 3, 130),
(2, 'MultiBuy', 2, 45);