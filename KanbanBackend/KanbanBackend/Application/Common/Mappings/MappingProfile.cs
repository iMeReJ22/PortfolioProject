using AutoMapper;
using KanbanBackend.Application.Boards.Queries.GetDashboardBoardsWithOwners;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Domain.Entities;

namespace KanbanBackend.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Board, BoardDto>()
                .ForMember(d => d.Columns, opt => opt.MapFrom(s => s.Columns))
                .ForMember(d => d.Tags, opt => opt.MapFrom(s => s.Tags))
                .ForMember(d => d.Members, opt => opt.MapFrom(s => s.BoardMembers));

            CreateMap<BoardMember, BoardMemberDto>();

            CreateMap<Column, ColumnDto>()
                .ForMember(d => d.Tasks, opt => opt.MapFrom(s => s.Tasks));

            CreateMap<Domain.Entities.Task, TaskDto>()
                .ForMember(d => d.Tags, opt => opt.MapFrom(s => s.Tags))
                .ForMember(d => d.Comments, opt => opt.MapFrom(s => s.TaskComments));

            CreateMap<TaskComment, TaskCommentDto>()
                .ForMember(d => d.AuthorName, opt => opt.MapFrom(s => s.Author != null ? s.Author.DisplayName : "No Author"));

            CreateMap<Tag, TagDto>();

            CreateMap<TaskType, TaskTypeDto>();

            CreateMap<Domain.Entities.ActivityLog, ActivityLogDto>()
                .ForMember(d => d.Desription, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.MemberId));


            CreateMap<User, UserDto>();

            CreateMap<Board, BoardTileDto>()
                .ForMember(d => d.Owner, opt => opt.MapFrom(s => s.Owner))
                .ForMember(d => d.boardMembers, opt => opt.MapFrom(s => s.BoardMembers));
        }
    }
}
