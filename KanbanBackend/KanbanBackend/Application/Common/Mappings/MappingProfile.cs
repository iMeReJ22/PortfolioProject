using AutoMapper;
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

            CreateMap<BoardMember, BoardMemberDto>()
                .ForMember(d => d.User, opt => opt.MapFrom(s => s.User));

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
                .ForMember(d => d.Desription, opt => opt.MapFrom(s => s.Description));


            CreateMap<User, UserDto>();

        }
    }
}
