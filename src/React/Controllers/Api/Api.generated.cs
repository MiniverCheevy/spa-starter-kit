/***************************************************************
//This code just called you a tool
//What I meant to say is that this code was generated by a tool
//so don't mess with it unless you're debugging
//subject to change without notice, might regenerate while you're reading, etc
***************************************************************/

using Web;
using Voodoo.Messages;
using Web.Infrastructure.ExecutionPipeline;
using Web.Infrastructure.ExecutionPipeline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Voodoo;
using Core.Operations.Users;
using Core.Operations.Users.Extras;
using Core.Operations.TestClasses;
using Core.Operations.TestClasses.Extras;
using Core.Operations.Teams;
using Core.Operations.Teams.Extras;
using Core.Operations.Projects;
using Core.Operations.Projects.Extras;
using Core.Operations.Members;
using Core.Operations.Members.Extras;
using Core.Operations.Lists;
using Core.Operations.Errors;
using Core.Operations.Errors.Extras;
using Core.Operations.CurrentUsers;
using Core.Identity;
using Core.Operations.ApplicationSettings;
using Core.Operations.ApplicationSettings.Extras;
namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class UserController : ApiControllerBase
    {
        [HttpDelete]
        public async Task<Response> Delete
        ( IdRequest request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <IdRequest, Response>
            {
                Command = new UserDeleteCommand(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] { "Administrator" } }
            };
            var pipeline = new ExcecutionPipeline<IdRequest, Response>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
        [HttpGet]
        public async Task<Response<UserDetail>> Get
        ( IdRequest request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <IdRequest, Response<UserDetail>>
            {
                Command = new UserDetailQuery(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] { "Administrator" } }
            };
            var pipeline = new ExcecutionPipeline<IdRequest, Response<UserDetail>>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
        [HttpPut]
        public async Task<NewItemResponse> Put
        ([FromBody] UserDetail request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <UserDetail, NewItemResponse>
            {
                Command = new UserSaveCommand(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] { "Administrator" } }
            };
            var pipeline = new ExcecutionPipeline<UserDetail, NewItemResponse>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
    }
    
    [Route("api/[controller]")]
    public class UserListController : ApiControllerBase
    {
        [HttpGet]
        public async Task<UserListResponse> Get
        ( UserListRequest request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <UserListRequest, UserListResponse>
            {
                Command = new UserListQuery(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] { "Administrator" } }
            };
            var pipeline = new ExcecutionPipeline<UserListRequest, UserListResponse>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
    }
    
    [Route("api/[controller]")]
    public class TestClassController : ApiControllerBase
    {
        [HttpDelete]
        public async Task<Response> Delete
        ( IdRequest request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <IdRequest, Response>
            {
                Command = new TestClassDeleteCommand(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] {  } }
            };
            var pipeline = new ExcecutionPipeline<IdRequest, Response>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
        [HttpGet]
        public async Task<Response<TestClassDetail>> Get
        ( IdRequest request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <IdRequest, Response<TestClassDetail>>
            {
                Command = new TestClassDetailQuery(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] {  } }
            };
            var pipeline = new ExcecutionPipeline<IdRequest, Response<TestClassDetail>>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
        [HttpPost]
        public async Task<NewItemResponse> Post
        ([FromBody] TestClassDetail request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <TestClassDetail, NewItemResponse>
            {
                Command = new TestClassSaveCommand(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] {  } }
            };
            var pipeline = new ExcecutionPipeline<TestClassDetail, NewItemResponse>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
    }
    
    [Route("api/[controller]")]
    public class TestClassListController : ApiControllerBase
    {
        [HttpGet]
        public async Task<TestClassListResponse> Get
        ( TestClassListRequest request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <TestClassListRequest, TestClassListResponse>
            {
                Command = new TestClassListQuery(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] {  } }
            };
            var pipeline = new ExcecutionPipeline<TestClassListRequest, TestClassListResponse>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
    }
    
    [Route("api/[controller]")]
    public class TeamController : ApiControllerBase
    {
        [HttpDelete]
        public async Task<Response> Delete
        ( IdRequest request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <IdRequest, Response>
            {
                Command = new TeamDeleteCommand(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] {  } }
            };
            var pipeline = new ExcecutionPipeline<IdRequest, Response>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
        [HttpGet]
        public async Task<Response<TeamDetail>> Get
        ( IdRequest request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <IdRequest, Response<TeamDetail>>
            {
                Command = new TeamDetailQuery(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] {  } }
            };
            var pipeline = new ExcecutionPipeline<IdRequest, Response<TeamDetail>>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
        [HttpPut]
        public async Task<NewItemResponse> Put
        ([FromBody] TeamDetail request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <TeamDetail, NewItemResponse>
            {
                Command = new TeamSaveCommand(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] {  } }
            };
            var pipeline = new ExcecutionPipeline<TeamDetail, NewItemResponse>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
    }
    
    [Route("api/[controller]")]
    public class TeamListController : ApiControllerBase
    {
        [HttpGet]
        public async Task<TeamListResponse> Get
        ( TeamListRequest request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <TeamListRequest, TeamListResponse>
            {
                Command = new TeamListQuery(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] {  } }
            };
            var pipeline = new ExcecutionPipeline<TeamListRequest, TeamListResponse>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
    }
    
    [Route("api/[controller]")]
    public class ProjectController : ApiControllerBase
    {
        [HttpDelete]
        public async Task<Response> Delete
        ( IdRequest request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <IdRequest, Response>
            {
                Command = new ProjectDeleteCommand(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] {  } }
            };
            var pipeline = new ExcecutionPipeline<IdRequest, Response>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
        [HttpGet]
        public async Task<Response<ProjectDetail>> Get
        ( IdRequest request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <IdRequest, Response<ProjectDetail>>
            {
                Command = new ProjectDetailQuery(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] {  } }
            };
            var pipeline = new ExcecutionPipeline<IdRequest, Response<ProjectDetail>>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
        [HttpPut]
        public async Task<NewItemResponse> Put
        ([FromBody] ProjectDetail request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <ProjectDetail, NewItemResponse>
            {
                Command = new ProjectSaveCommand(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] {  } }
            };
            var pipeline = new ExcecutionPipeline<ProjectDetail, NewItemResponse>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
    }
    
    [Route("api/[controller]")]
    public class ProjectListController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ProjectListResponse> Get
        ( ProjectListRequest request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <ProjectListRequest, ProjectListResponse>
            {
                Command = new ProjectListQuery(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] {  } }
            };
            var pipeline = new ExcecutionPipeline<ProjectListRequest, ProjectListResponse>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
    }
    
    [Route("api/[controller]")]
    public class MemberController : ApiControllerBase
    {
        [HttpDelete]
        public async Task<Response> Delete
        ( IdRequest request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <IdRequest, Response>
            {
                Command = new MemberDeleteCommand(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] {  } }
            };
            var pipeline = new ExcecutionPipeline<IdRequest, Response>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
        [HttpGet]
        public async Task<Response<MemberDetail>> Get
        ( IdRequest request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <IdRequest, Response<MemberDetail>>
            {
                Command = new MemberDetailQuery(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] {  } }
            };
            var pipeline = new ExcecutionPipeline<IdRequest, Response<MemberDetail>>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
        [HttpPut]
        public async Task<NewItemResponse> Put
        ([FromBody] MemberDetail request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <MemberDetail, NewItemResponse>
            {
                Command = new MemberSaveCommand(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] {  } }
            };
            var pipeline = new ExcecutionPipeline<MemberDetail, NewItemResponse>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
    }
    
    [Route("api/[controller]")]
    public class MemberListController : ApiControllerBase
    {
        [HttpGet]
        public async Task<MemberListResponse> Get
        ( MemberListRequest request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <MemberListRequest, MemberListResponse>
            {
                Command = new MemberListQuery(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] {  } }
            };
            var pipeline = new ExcecutionPipeline<MemberListRequest, MemberListResponse>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
    }
    
    [Route("api/[controller]")]
    public class ListsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ListsResponse> Get
        ( ListsRequest request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <ListsRequest, ListsResponse>
            {
                Command = new LookupsQuery(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] {  } }
            };
            var pipeline = new ExcecutionPipeline<ListsRequest, ListsResponse>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
    }
    
    [Route("api/[controller]")]
    public class ErrorLogController : ApiControllerBase
    {
        [HttpGet]
        public async Task<Response<ErrorDetail>> Get
        ( IdRequest request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <IdRequest, Response<ErrorDetail>>
            {
                Command = new ErrorDetailQuery(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] {  } }
            };
            var pipeline = new ExcecutionPipeline<IdRequest, Response<ErrorDetail>>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
    }
    
    [Route("api/[controller]")]
    public class ErrorLogListController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ErrorListResponse> Get
        ( ErrorListRequest request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <ErrorListRequest, ErrorListResponse>
            {
                Command = new ErrorListQuery(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] {  } }
            };
            var pipeline = new ExcecutionPipeline<ErrorListRequest, ErrorListResponse>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
    }
    
    [Route("api/[controller]")]
    public class MobileErrorController : ApiControllerBase
    {
        [HttpPost]
        public async Task<Response> Post
        ([FromBody] MobileErrorRequest request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <MobileErrorRequest, Response>
            {
                Command = new MobileErrorAddCommand(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = true, Roles=new string[] {  } }
            };
            var pipeline = new ExcecutionPipeline<MobileErrorRequest, Response>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
    }
    
    [Route("api/[controller]")]
    public class CurrentUserController : ApiControllerBase
    {
        [HttpGet]
        public async Task<Response<AppPrincipal>> Get
        ( EmptyRequest request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <EmptyRequest, Response<AppPrincipal>>
            {
                Command = new GetCurrentUserCommand(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = true, Roles=new string[] {  } }
            };
            var pipeline = new ExcecutionPipeline<EmptyRequest, Response<AppPrincipal>>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
    }
    
    [Route("api/[controller]")]
    public class ApplicationSettingController : ApiControllerBase
    {
        [HttpDelete]
        public async Task<Response> Delete
        ( IdRequest request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <IdRequest, Response>
            {
                Command = new ApplicationSettingDeleteCommand(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] {  } }
            };
            var pipeline = new ExcecutionPipeline<IdRequest, Response>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
        [HttpGet]
        public async Task<Response<ApplicationSettingDetail>> Get
        ( IdRequest request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <IdRequest, Response<ApplicationSettingDetail>>
            {
                Command = new ApplicationSettingDetailQuery(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] {  } }
            };
            var pipeline = new ExcecutionPipeline<IdRequest, Response<ApplicationSettingDetail>>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
        [HttpPut]
        public async Task<NewItemResponse> Put
        ([FromBody] ApplicationSettingDetail request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <ApplicationSettingDetail, NewItemResponse>
            {
                Command = new ApplicationSettingSaveCommand(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] {  } }
            };
            var pipeline = new ExcecutionPipeline<ApplicationSettingDetail, NewItemResponse>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
    }
    
    [Route("api/[controller]")]
    public class ApplicationSettingListController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ApplicationSettingListResponse> Get
        ( ApplicationSettingListRequest request)
        {
            var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
            <ApplicationSettingListRequest, ApplicationSettingListResponse>
            {
                Command = new ApplicationSettingListQuery(request),
                Context = HttpContext,
                ModelState = ModelState,
                Request = request,
                SecurityContext = new SecurityContext { AllowAnonymouse = false, Roles=new string[] {  } }
            };
            var pipeline = new ExcecutionPipeline<ApplicationSettingListRequest, ApplicationSettingListResponse>
            (state);
            await pipeline.ExecuteAsync();
            return state.Response;
        }
    }
}

