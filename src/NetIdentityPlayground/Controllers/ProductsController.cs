using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace NetIdentityPlayground.Controllers
{
    /// <summary>
    /// Controlador que administra productos (ejemplo base para CRUD).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        /// <summary>
        /// Obtiene la lista completa de productos.
        /// </summary>
        /// <returns>Retorna la colección de productos</returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtiene todos los productos",
            Description = "Recupera la lista completa de productos almacenados."
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Devuelve la lista de productos")]
        public ActionResult<IEnumerable<ProductDto>> GetAllProducts()
        {
            // Ejemplo ficticio de obtener una lista de productos desde la capa de servicio
            var products = new List<ProductDto>
            {
                new ProductDto { Id = 1, Name = "Producto A", Price = 100 },
                new ProductDto { Id = 2, Name = "Producto B", Price = 200 }
            };

            return Ok(products);
        }

        /// <summary>
        /// Obtiene un producto por su identificador único (ID).
        /// </summary>
        /// <param name="id">ID del producto a consultar.</param>
        /// <returns>Retorna el producto encontrado o un 404 si no existe.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obtiene un producto específico",
            Description = "Devuelve la información de un producto dado su ID."
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Devuelve el producto especificado")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No se encontró el producto con el ID proporcionado")]
        public ActionResult<ProductDto> GetProductById(int id)
        {
            // Ejemplo ficticio de obtener un producto
            var product = new ProductDto { Id = id, Name = "Producto X", Price = 999 };

            // Simulamos que si el Id es 0, no existe
            if (id == 0)
            {
                return NotFound($"El producto con Id = {id} no fue encontrado.");
            }

            return Ok(product);
        }

        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        /// <param name="newProduct">Objeto con la información del producto a crear.</param>
        /// <returns>Retorna la información del producto creado y la ruta para obtenerlo.</returns>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Crea un nuevo producto",
            Description = "Agrega un producto a la base de datos con la información proporcionada."
        )]
        [SwaggerResponse(StatusCodes.Status201Created, "Producto creado exitosamente")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "La petición es inválida o faltan datos")]
        public ActionResult<ProductDto> CreateProduct([FromBody] CreateProductDto newProduct)
        {
            // Validación de ejemplo
            if (newProduct == null || string.IsNullOrWhiteSpace(newProduct.Name))
            {
                return BadRequest("La información del producto no es válida.");
            }

            // Simulamos guardar el producto y generar un Id
            var createdProduct = new ProductDto
            {
                Id = 999, // Asignar un ID ficticio
                Name = newProduct.Name,
                Price = newProduct.Price
            };

            // Ejemplo de CreatedAtAction para devolver la ruta del recurso recién creado
            return CreatedAtAction(
                nameof(GetProductById),
                new { id = createdProduct.Id },
                createdProduct
            );
        }

        /// <summary>
        /// Actualiza la información de un producto existente.
        /// </summary>
        /// <param name="id">ID del producto que se desea actualizar.</param>
        /// <param name="updateData">Datos nuevos para actualizar el producto.</param>
        /// <returns>Retorna la información del producto actualizada o 404 si no se encontró.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Actualiza un producto existente",
            Description = "Modifica los campos de un producto específico con la información proporcionada."
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Producto actualizado correctamente")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No se encontró el producto para actualizar")]
        public ActionResult<ProductDto> UpdateProduct(int id, [FromBody] UpdateProductDto updateData)
        {
            // Ejemplo ficticio: suponer que buscamos el producto en la BD
            var product = new ProductDto { Id = id, Name = "Producto X", Price = 999 };

            if (id == 0 || product == null)
            {
                return NotFound($"El producto con Id = {id} no fue encontrado.");
            }

            // Actualizar datos
            product.Name = updateData.Name;
            product.Price = updateData.Price;

            // Retorna el producto actualizado
            return Ok(product);
        }

        /// <summary>
        /// Elimina un producto de la lista.
        /// </summary>
        /// <param name="id">ID del producto que se desea eliminar.</param>
        /// <returns>Retorna un estado 204 si el borrado fue exitoso, o 404 si no se encontró el producto.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Elimina un producto existente",
            Description = "Remueve un producto de la base de datos si existe."
        )]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Producto eliminado con éxito")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No se encontró el producto para eliminar")]
        public IActionResult DeleteProduct(int id)
        {
            // Simulamos que el producto no existe si el id = 0
            if (id == 0)
            {
                return NotFound($"El producto con Id = {id} no fue encontrado.");
            }

            // Lógica para eliminar el producto de la BD ...

            return NoContent();
        }
    }

    // ====================== Ejemplos de Dtos ======================
    // DTO (Data Transfer Object) usado para devolver información de un producto
    public class ProductDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
    }

    // DTO para crear productos (puedes agregar validaciones como [Required], [StringLength], etc.)
    public class CreateProductDto
    {
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
    }

    // DTO para actualizar productos
    public class UpdateProductDto
    {
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
    }
}