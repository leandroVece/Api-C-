using API.Service;
using EF.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }


    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_categoryService.Get());
    }



    [HttpPost]
    public IActionResult Post([FromBody] Category category)
    {
        _categoryService.Post(category);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] Category category)
    {
        _categoryService.update(id, category);
        return Ok();
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _categoryService.Delete(id);
        return Ok();
    }

}