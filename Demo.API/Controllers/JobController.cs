using System.Data;
using System.Threading.Tasks;
using Dapper;
using Demo.API.Dto;
using Demo.API.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
namespace Demo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : ControllerBase
    {
       public JobController()
        {

        }

         [HttpGet]
        public async Task<IActionResult> GetJob()
        {
            using (IDbConnection dbConnection = CreateConnection())
            {
                if (dbConnection.State != ConnectionState.Closed)
                    dbConnection.Open();

                var sql = "SELECT * FROM job";

                var job = await dbConnection.QueryAsync<JobPostion>(sql);

                return Ok(job);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJob(int id)
        {
           try
            {
                ArgumentOutOfRangeException.ThrowIfNegative(id);

            using (IDbConnection dbConnection = CreateConnection())
            {
                if (dbConnection.State != ConnectionState.Closed)
                    dbConnection.Open();

                var sql = "SELECT * FROM job where id = @id";

                var job = await dbConnection.QueryFirstOrDefaultAsync<JobPostion>(sql, new { id });

                return Ok(job);
            }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [HttpPost("ADD")]
        public async Task<IActionResult> CreateJobPosition(CreateJobPosition jobPosition)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(jobPosition);
                ArgumentException.ThrowIfNullOrEmpty(jobPosition.name);
                ArgumentException.ThrowIfNullOrEmpty(jobPosition.beginning_salary.ToString());

                 using (IDbConnection dbConnection = CreateConnection())
            {
                if (dbConnection.State != ConnectionState.Closed)
                    dbConnection.Open();

                var sql = $@"
                INSERT INTO job 
                    (name, beginning_salary) 
                VALUES
                     (@name, @beginning_salary);";

                try
                {
                    await dbConnection.ExecuteAsync(sql, jobPosition);

                    return Ok("Job created successfully.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }

            }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


[HttpPut("UPDATE")]
public async Task<IActionResult> UpdateJobPosition(UpdateJob jobPosition)
{
    try
    {
        ArgumentNullException.ThrowIfNull(jobPosition);
        ArgumentException.ThrowIfNullOrEmpty(jobPosition.name);

        if (jobPosition.beginning_salary <= 0)
            return BadRequest("Beginning salary must be greater than 0.");

        if (jobPosition.id <= 0)
            return BadRequest("Invalid job position ID.");

        using (IDbConnection dbConnection = CreateConnection())
        {
            if (dbConnection.State != ConnectionState.Open)
                dbConnection.Open();

            var sql = @"
                UPDATE job 
                SET 
                    name = @name, 
                    beginning_salary = @beginning_salary 
                WHERE 
                    id = @id;
            ";

            try
            {
                int rowsAffected = await dbConnection.ExecuteAsync(sql, jobPosition);

                if (rowsAffected == 0)
                    return NotFound("Job position not found.");

                return Ok("Job updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
    catch (Exception ex)
    {
        return BadRequest(ex.ToString());
    }
}


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {

            using (IDbConnection dbConnection = CreateConnection())
            {
                if (dbConnection.State != ConnectionState.Closed)
                    dbConnection.Open();

                var sql = "DELETE FROM job where id = @id";

                var affectedRows = await dbConnection.ExecuteAsync(sql, new { id });

                if(affectedRows > 0)
                {
                    return Ok("Job deleted successfully.");
                }
                else
                {
                    return NotFound("job not found.");
                }
            }

        }

        private IDbConnection CreateConnection()
        {
            return new MySqlConnection("server=localhost;port=3306;database=dapperdemodb;user=root;password=mandawe123;");
        }
    }
}