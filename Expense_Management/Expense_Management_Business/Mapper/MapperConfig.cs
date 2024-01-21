using AutoMapper;
using Expense_Management_Data.Entities;
using Expense_Management_Schema.Categories.Requests;
using Expense_Management_Schema.Categories.Responses;
using Expense_Management_Schema.EFTDetails.Requests;
using Expense_Management_Schema.EFTDetails.Responses;
using Expense_Management_Schema.Expenses.Requests;
using Expense_Management_Schema.Expenses.Responses;
using Expense_Management_Schema.Notifications.Requests;
using Expense_Management_Schema.Notifications.Responses;
using Expense_Management_Schema.Payments.Requests;
using Expense_Management_Schema.Payments.Responses;
using Expense_Management_Schema.Reports.Requests;
using Expense_Management_Schema.Reports.Responses;
using Expense_Management_Schema.Users.Requests;
using Expense_Management_Schema.Users.Responses;

namespace Expense_Management_Business.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<UserRequest, User>();
        CreateMap<User, UserResponse>().ForMember(dest => dest.UserFullName,
                opt => opt.MapFrom(src => src.UserFirstName + " " + src.UserLastName))
            .ForMember(x => x.Role,
                opt => opt.MapFrom(src => (RoleEnum)src.Role));


        CreateMap<ExpenseRequest, Expense>();
        CreateMap<Expense, ExpenseResponse>()
            .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src =>
                    src.User != null ? $"{src.User.UserFirstName} {src.User.UserLastName}" : null))
            .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(x => x.Status,
                opt => opt.MapFrom(src => (ExpenseStatus)src.Status));


        CreateMap<CategoryRequest, Category>();
        CreateMap<Category, CategoryResponse>();


        CreateMap<ReportRequest, Report>();
        CreateMap<Report, ReportResponse>()
            // .ForMember(dest => dest.CreatedByUserName,
            //     opt => opt.MapFrom(src =>
            //         src.CreatedByUser != null
            //             ? $"{src.CreatedByUser.UserFirstName} {src.CreatedByUser.UserLastName}"
            //             : null))
            .ForMember(x => x.Status,
                opt => opt.MapFrom(src => (ExpenseStatus)src.Status))
            .ForMember(x => x.CategoryName,
                opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(x => x.Date,
                opt => opt.MapFrom(src => src.Date.Date));

        CreateMap<PaymentRequest, Payment>();
        CreateMap<Payment, PaymentResponse>()
            .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src =>
                    src.User != null ? $"{src.User.UserFirstName} {src.User.UserLastName}" : null))
            .ForMember(x => x.Status,
                opt => opt.MapFrom(src => (PaymentStatus)src.Status));

        CreateMap<NotificationRequest, Notification>();
        CreateMap<Notification, NotificationResponse>()
            .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src =>
                    src.User != null ? $"{src.User.UserFirstName} {src.User.UserLastName}" : null))
            .ForMember(x => x.DateCreated,
                opt =>
                    opt.MapFrom(src => src.DateCreated.Date.ToString(" dd.MM.yyyy")));

        CreateMap<EFTDetailRequest, EFTDetail>();
        CreateMap<EFTDetail, EFTDetailResponse>()
            .ForMember(x => x.TransactionDate,
                opt =>
                    opt.MapFrom(src => src.TransactionDate.Date.ToString("dd.MM.yyyy")));
    }
}