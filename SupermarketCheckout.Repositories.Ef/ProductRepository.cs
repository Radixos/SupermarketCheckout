﻿using Microsoft.EntityFrameworkCore;
using SupermarketCheckout.Model;
using SupermarketCheckout.Model.Exceptions;
using SupermarketCheckout.Model.Repositories;
using SupermarketCheckout.Repositories.Ef.Entities;
using SupermarketCheckout.Repositories.Ef.Mappers;

namespace SupermarketCheckout.Repositories.Ef
{
    public class ProductRepository : IProductRepository
    {
        private readonly SupermarketContext _context;

        public ProductRepository(SupermarketContext context)
        {
            _context = context
                ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var productEntities = await _context.Product
                .Include(p => p.Offer)
                .ToListAsync();

            if (productEntities == null || !productEntities.Any()) {
                throw new NotFoundException();
            }

            return productEntities.Select(ProductMapper.MapToProduct).ToList();
        }

        public async Task<Product> GetProductAsync(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException("Cannot be null or white space", nameof(sku));
            }

            var product = await _context.Product
                .Where(p => p.Sku == sku)
                .Select(product => new
                {
                    product.Sku,
                    product.Price,
                    Offer = product.OfferId != null
                        ? _context.Offer
                            .Where(o => o.OfferId == product.OfferId)
                            .Select(o => new
                            {
                                o.OfferType,
                                o.OfferQuantity,
                                o.OfferPrice
                            })
                            .FirstOrDefault()
                        : null
                }).FirstOrDefaultAsync();

            if (product == null)
            {
                throw new NotFoundException($"Product with Sku '{sku}' doesn't exist.");
            }

            Offer? offer = null;

            if (product.Offer != null)
            {
                var offerFactory = new OfferFactory();
                offer = offerFactory.CreateOffer(
                    product.Offer.OfferType,
                    product.Offer.OfferQuantity ?? 0,
                    product.Offer.OfferPrice ?? 0
                );
            }

            return new Product(product.Sku, product.Price, offer);
        }

        public async Task AddProductAsync(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var existingProduct = await _context.Product.FirstOrDefaultAsync(p => p.Sku == product.Sku);

            if (existingProduct != null)
            {
                throw new InvalidOperationException(nameof(existingProduct));
            }

            OfferEntity? offerEntity = null;

            if (!string.IsNullOrWhiteSpace(product.Offer?.OfferType))
            {
                offerEntity = await _context.Offer.FirstOrDefaultAsync(o => o.OfferType == product.Offer.OfferType);
            }

            var newProduct = new ProductEntity
            {
                Sku = product.Sku,
                Price = product.Price,
                OfferId = offerEntity?.OfferId,
                Offer = offerEntity
            };

            _context.Product.Add(newProduct);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var existingProduct = await _context.Product
                .FirstOrDefaultAsync(p => p.Sku == product.Sku);

            if (existingProduct == null)
            {
                throw new NotFoundException($"Product with SKU '{product.Sku}' was not found.");
            }

            _context.Product.Remove(existingProduct);

            await _context.SaveChangesAsync();
        }
    }
}
