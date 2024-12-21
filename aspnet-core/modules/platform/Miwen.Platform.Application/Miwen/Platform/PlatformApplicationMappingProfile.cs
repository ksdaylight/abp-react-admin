using AutoMapper;
using Miwen.Platform.Datas;
using Miwen.Platform.Feedbacks;
using Miwen.Platform.Layouts;
using Miwen.Platform.Menus;
using Miwen.Platform.Packages;

namespace Miwen.Platform;

public class PlatformApplicationMappingProfile : Profile
{
    public PlatformApplicationMappingProfile()
    {
        CreateMap<PackageBlob, PackageBlobDto>();
        CreateMap<Package, PackageDto>();

        CreateMap<DataItem, DataItemDto>();
        CreateMap<Data, DataDto>();
        CreateMap<Menu, MenuDto>()
            .ForMember(dto => dto.Meta, map => map.MapFrom(src => src.ExtraProperties))
            .ForMember(dto => dto.Startup, map => map.Ignore());
        CreateMap<Layout, LayoutDto>()
            .ForMember(dto => dto.Meta, map => map.MapFrom(src => src.ExtraProperties));
        CreateMap<UserFavoriteMenu, UserFavoriteMenuDto>();

        CreateMap<Feedback, FeedbackDto>();
        CreateMap<FeedbackComment, FeedbackCommentDto>();
        CreateMap<FeedbackAttachment, FeedbackAttachmentDto>();
    }
}
