using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiVersion("1.0")]
[ApiController]
// [Route("api/[controller]")]
[Route("api/v{version:apiVersion}/products")]
public class ProductV1Controller : ControllerBase
{
    private readonly ShopContext _context;
    public ProductV1Controller(ShopContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }

    [HttpGet]
    public async Task<IEnumerable<Product>> GetAllProduct([FromQuery] ProductQueryParameters queryParameters)
    {
        IQueryable<Product> products = _context.Products;

        if (!string.IsNullOrEmpty(queryParameters.ProductName))
        {
            products = products.Where(p => p.Name.Contains(queryParameters.ProductName));
        }

        if (!string.IsNullOrEmpty(queryParameters.sku))
        {
            products = products.Where(p => p.Sku.ToLower().Contains(queryParameters.sku.ToLower()));
        }

        if (queryParameters.MinPrice != null)
        {
            products = products.Where(p => p.Price >= queryParameters.MinPrice);
        }
        if (queryParameters.MaxPrice != null)
        {
            products = products.Where(p => p.Price <= queryParameters.MaxPrice);
        }
        if (!string.IsNullOrEmpty(queryParameters.SortBy))
        {
            if (typeof(Product).GetProperty(queryParameters.SortBy) != null)
            {
                products = products.OrderByCustom(queryParameters.SortBy, queryParameters.SortOrder);
            }
        }

        if (!string.IsNullOrEmpty(queryParameters.SearchTerm))
        {
            products = products.Where(p =>
                p.Name.ToLower().Contains(queryParameters.SearchTerm.ToLower())
             || p.Sku.ToLower().Contains(queryParameters.SearchTerm.ToLower()));
        }

        products = products.Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
            .Take(queryParameters.PageSize);
        return await products.ToArrayAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> getProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> PostProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(getProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutProduct(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }
        _context.Entry(product).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Products.Any(e => e.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost]
    [Route("delete")]
    public async Task<ActionResult> DeleteProducts([FromQuery] int[] ids)
    {
        var products = new List<Product>();
        foreach (var id in ids)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            products.Add(product);
        }
        _context.Products.RemoveRange(products);
        await _context.SaveChangesAsync();
        return Ok(products);
    }
}

[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/products")]
[ApiController]
public class ProductV2Controller : ControllerBase
{
    private readonly ShopContext _context;
    public ProductV2Controller(ShopContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }

    [HttpGet]
    public async Task<IEnumerable<Product>> GetAllProduct([FromQuery] ProductQueryParameters queryParameters)
    {
        IQueryable<Product> products = _context.Products.Where(p => p.IsAvailable == true);

        if (!string.IsNullOrEmpty(queryParameters.ProductName))
        {
            products = products.Where(p => p.Name.Contains(queryParameters.ProductName));
        }

        if (!string.IsNullOrEmpty(queryParameters.sku))
        {
            products = products.Where(p => p.Sku.ToLower().Contains(queryParameters.sku.ToLower()));
        }

        if (queryParameters.MinPrice != null)
        {
            products = products.Where(p => p.Price >= queryParameters.MinPrice);
        }
        if (queryParameters.MaxPrice != null)
        {
            products = products.Where(p => p.Price <= queryParameters.MaxPrice);
        }
        if (!string.IsNullOrEmpty(queryParameters.SortBy))
        {
            if (typeof(Product).GetProperty(queryParameters.SortBy) != null)
            {
                products = products.OrderByCustom(queryParameters.SortBy, queryParameters.SortOrder);
            }
        }

        if (!string.IsNullOrEmpty(queryParameters.SearchTerm))
        {
            products = products.Where(p =>
                p.Name.ToLower().Contains(queryParameters.SearchTerm.ToLower())
             || p.Sku.ToLower().Contains(queryParameters.SearchTerm.ToLower()));
        }

        products = products.Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
            .Take(queryParameters.PageSize);
        return await products.ToArrayAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> getProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> PostProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(getProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutProduct(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }
        _context.Entry(product).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Products.Any(e => e.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost]
    [Route("delete")]
    public async Task<ActionResult> DeleteProducts([FromQuery] int[] ids)
    {
        var products = new List<Product>();
        foreach (var id in ids)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            products.Add(product);
        }
        _context.Products.RemoveRange(products);
        await _context.SaveChangesAsync();
        return Ok(products);
    }
}
