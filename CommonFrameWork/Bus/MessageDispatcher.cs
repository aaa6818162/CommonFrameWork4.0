using System.Threading.Tasks;
using CommonFrameWork.Bus.Distributed;
using CommonFrameWork.Bus.Local;
using CommonFrameWork.CommUtils;
using CommonFrameWork.Dependency;

namespace CommonFrameWork.Bus
{
    public interface IMessageDispatcher
    {
        string Dispatch<TMessage>(TMessage msg)
            where TMessage : class, IMessage;

        Task<string> DispatchAsync<TMessage>(TMessage msg)
            where TMessage : class, IMessage;

    }

    /// <summary>
    /// 
    /// </summary>
    public class MessageDispatcher : IMessageDispatcher
    {

        #region -  Field(s)  -
        private readonly ILocalMessageBus _localMessageBus;
        #endregion


        #region -  Constructor(s)  -

        public MessageDispatcher(ILocalMessageBus localMessageBus)
        {
            _localMessageBus = localMessageBus;

        }

        #endregion

        #region -  Dispatch  -

        public string Dispatch<TMessage>(TMessage msg)
            where TMessage : class, IMessage
        {
            var dispatchMode = msg.MessageDispatchModeEnum;
            if (dispatchMode == MessageDispatchModeEnum.DistributedSend)
            {
                AsyncUtils.RunSync(ObjectContainer.Resolve<IDistributedMessageBus>().SendAsync(msg));
            }
            else if (dispatchMode == MessageDispatchModeEnum.DistributedPublish)
            {
                AsyncUtils.RunSync(ObjectContainer.Resolve<IDistributedMessageBus>().PublishAsync(msg));
            }
            else if (dispatchMode == MessageDispatchModeEnum.DistributedRequest)
            {
                var msgCode = AsyncUtils.RunSync(ObjectContainer.Resolve<IDistributedMessageBus>().RequestAsync(msg));
                return msgCode;
            }
            else if (dispatchMode == MessageDispatchModeEnum.Local)
            {
                string msgCode;
                try
                {
                    msgCode = AsyncUtils.RunSync(_localMessageBus.ExecuteAsync(msg));
                }
                catch (System.Exception ex)
                {
                    msgCode = ex.GetBaseException().Message;
                }

                return msgCode;
            }
            return "";
        }

        public async Task<string> DispatchAsync<TMessage>(TMessage msg)
           where TMessage : class, IMessage
        {
            var dispatchMode = msg.MessageDispatchModeEnum;
            if (dispatchMode == MessageDispatchModeEnum.DistributedSend)
            {
                await ObjectContainer.Resolve<IDistributedMessageBus>().SendAsync(msg).ConfigureAwait(false);
            }
            else if (dispatchMode == MessageDispatchModeEnum.DistributedPublish)
            {

                await ObjectContainer.Resolve<IDistributedMessageBus>().PublishAsync(msg).ConfigureAwait(false);
            }
            else if (dispatchMode == MessageDispatchModeEnum.DistributedRequest)
            {
                var msgCode = await ObjectContainer.Resolve<IDistributedMessageBus>().RequestAsync(msg).ConfigureAwait(false);
                return msgCode;
            }
            else if (dispatchMode == MessageDispatchModeEnum.Local)
            {
                string msgCode;
                try
                {
                    msgCode = await _localMessageBus.ExecuteAsync(msg).ConfigureAwait(false);
                }
                catch (System.Exception ex)
                {
                    msgCode = ex.GetBaseException().Message;
                }

                return msgCode;
            }
            return "";
        }


        #endregion
    }



}
