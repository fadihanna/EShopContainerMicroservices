namespace Magic.Application.Dtos
{
public record NotificationDto
  (
          int Id  

        , string Title  

        , string Description 

        , string IconName  

         ,int Status 

         , DateTime NotificationTime  

         , int SortOrder  
);
}
