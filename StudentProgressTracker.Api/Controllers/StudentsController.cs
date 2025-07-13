using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentProgressTracker.Core;
using StudentProgressTracker.Core.Application.Courses.Add;
using StudentProgressTracker.Core.Application.Courses.Details;
using StudentProgressTracker.Core.Application.Courses.List;
using StudentProgressTracker.Shared.Ids;
using StudentProgressTracker.Shared.Paging;

namespace StudentProgressTracker.Api.Controllers;

[ApiController]
public class StudentsController(ISender sender) : ControllerBase
{
    /// <summary>
    /// add student
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost(ApiRoutes.Students.Add)]
    public async Task<IActionResult> Create(
        [FromBody] AddStudentCommand command,
        CancellationToken cancellationToken = default
    )
    {
        await sender.Send(command, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// get student details
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet(ApiRoutes.Students.Details)]
    public async Task<ActionResult<GetStudentDetailsQueryResult>> Details(
        [FromRoute] StudentId id,
        CancellationToken cancellationToken = default
    )
    {
        var result = await sender.Send(new GetStudentDetailsQuery(id), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// get students list
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet(ApiRoutes.Students.List)]
    public async Task<ActionResult<PageResponse<GetStudentListQueryResult>>> List(
        [FromQuery] GetStudentListQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var result = await sender.Send(query, cancellationToken);
        return Ok(result);
    }
}
