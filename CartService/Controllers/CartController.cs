using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CartService.Data;
using CartService.Models;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly CartContext _context;

    public CartController(CartContext context)
    {
        _context = context;
    }

    // GET: api/cart/{userId}
    [HttpGet("{userId}")]
    public async Task<ActionResult<Cart>> GetCart(string userId)
    {
        var cart = await _context.Carts.Include(c => c.Items)
                                       .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
        {
            return NotFound();
        }

        return cart;
    }

    // POST: api/cart
    [HttpPost]
    public async Task<ActionResult<Cart>> CreateCart(Cart cart)
    {
        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCart), new { userId = cart.UserId }, cart);
    }

    // PUT: api/cart/{userId}
    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateCart(string userId, Cart updatedCart)
    {
        var cart = await _context.Carts.Include(c => c.Items)
                                       .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
        {
            return NotFound();
        }

        cart.Items = updatedCart.Items;
        _context.Entry(cart).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/cart/{userId}
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteCart(string userId)
    {
        var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
        if (cart == null)
        {
            return NotFound();
        }

        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
