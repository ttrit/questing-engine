using AutoMapper;
using MongoDB.Bson;
using QuestingEngine.Model;

namespace QuestingEngine.API.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Repository.DbModels.Player, Player>()
                .ForMember(des => des.CurrentQuest, act => act.Ignore())
                .ForMember(des => des.CompletedMilestones, act => act.Ignore());
            CreateMap<Repository.DbModels.Milestone, Milestone>();
            CreateMap<Repository.DbModels.Quest, Quest>()
                .ForMember(des => des.Milestones, act => act.Ignore());
            

            CreateMap<Player, Repository.DbModels.Player>()
                .ForMember(des => des.CurrentQuest, act => act.MapFrom(src => new ObjectId(src.CurrentQuest.Id)))
                .ForMember(des => des.CompletedMilestones, act => act.Ignore());
            CreateMap<Quest, Repository.DbModels.Quest>()
                .ForMember(des => des.Milestones, act => act.Ignore());
            CreateMap<Milestone, Repository.DbModels.Milestone>()
                .ForMember(des => des.Id, act => act.MapFrom(src => new ObjectId(src.Id)));
        }
    }
}
