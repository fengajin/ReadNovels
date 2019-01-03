using System;

using Unity;
using ReadNovels.Service;
using ReadNovels.IService;

namespace ReadNovels.WebApi
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
            //
            container.RegisterType<IWX_UserDAL, WX_UserDAL>();
            container.RegisterType<INovelsService, NovelsService>();
            //
            container.RegisterType<ICaptureService, CaptureService>();


            container.RegisterType<IReadinglogService, ReadinglogService>();

            container.RegisterType<IManagerService, ManagerService>();
            //小说书库
            container.RegisterType<IBookScarkService, BookScarkService>();


            container.RegisterType<INovelAddedService, NovelAddedService>();
            //小说阅读
            container.RegisterType<IReadService, ReadService>();

            //小说抓取、保存
            container.RegisterType<ICaptureService, CaptureService>();
            //小说详情
            container.RegisterType<IDetailsNovelServices, DetailsNovelServices>();
            //小说吐槽
            container.RegisterType<ICommentsService, CommentsService>();
            //小说书架
            container.RegisterType<IBookRackService, BookRackService>();
            //抓取小说表更新
            container.RegisterType<ICaptureUpdateService, CaptureUpdateService>();
            //小说收藏
            container.RegisterType<ICollectService, CollectService>();
            //小说审核
            container.RegisterType<INovelAuditService, NovelAuditService>();
            //权限管理
            container.RegisterType<IPowerService, PowerService>();
            container.RegisterType<IRoleService, RoleService>();

        }
    }
}