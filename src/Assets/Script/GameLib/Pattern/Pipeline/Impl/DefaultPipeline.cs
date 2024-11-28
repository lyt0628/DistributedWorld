using System;

namespace QS.GameLib.Pattern.Pipeline
{
    internal class DefaultPipeline : IPipeline
    {
        public DefaultPipelineHandlerContext root;
        public DefaultPipelineHandlerContext tail;
        public void AddAfter(string baseName, string name, IPipelineHandler handler)
        {
            DefaultPipelineHandlerContext node = root;
            DefaultPipelineHandlerContext pre = default;
            while (node != null)
            {
                node = node.NextHandlerContext;

                if (node.HandlerName == baseName)
                {
                    pre = node;
                }
                else if (node.HandlerName == name)
                {
                    throw new ArgumentException();
                }
            }
            if (pre == null)
            {
                throw new InvalidOperationException();
            }
            DoAddAfter(name, handler, pre);
        }

        private void DoAddAfter(string name, IPipelineHandler handler, DefaultPipelineHandlerContext pre)
        {
            DefaultPipelineHandlerContext next = pre.NextHandlerContext;
            DefaultPipelineHandlerContext n = new(
                this, name, handler, pre, next);
            pre.NextHandlerContext = n;
            next.PreHandlerContext = n;

            if (n.NextHandlerContext == null)
            {
                tail = n;
            }
        }

        public void AddBefore(string baseName, string name, IPipelineHandler handler)
        {

            DefaultPipelineHandlerContext node = root;
            DefaultPipelineHandlerContext next = default;
            while (node != null)
            {
                node = node.NextHandlerContext;

                if (node.HandlerName == baseName)
                {
                    next = node;
                }
                else if (node.HandlerName == name)
                {
                    throw new ArgumentException();
                }
            }
            if (next == null)
            {
                throw new InvalidOperationException();
            }
            DoAddBefore(name, handler, next);
        }

        private void DoAddBefore(string name, IPipelineHandler handler, DefaultPipelineHandlerContext next)
        {
            DefaultPipelineHandlerContext pre = next.PreHandlerContext;
            DefaultPipelineHandlerContext n = new(
                this, name, handler, pre, next);
            next.PreHandlerContext = n;
            pre.NextHandlerContext = n;

            if (n.PreHandlerContext == null)
            {
                root = n;
            }
        }

        public void AddFirst(string name, IPipelineHandler handler)
        {
            var handlerCtx = new DefaultPipelineHandlerContext(this, name, handler);
            if (root == null)
            {
                tail = root = handlerCtx;
            }
            else
            {
                DoAddFirst(handlerCtx);
            }
        }

        private void DoAddFirst(DefaultPipelineHandlerContext handlerCtx)
        {
            DefaultPipelineHandlerContext next = root.NextHandlerContext;
            DefaultPipelineHandlerContext pre = root;
            root.NextHandlerContext = handlerCtx;
            next.PreHandlerContext = handlerCtx;
            handlerCtx.NextHandlerContext = next;
            handlerCtx.PreHandlerContext = pre;

            root = handlerCtx;
        }

        public void AddLast(string name, IPipelineHandler handler)
        {
            var handlerCtx = new DefaultPipelineHandlerContext(this, name, handler);
            if (tail == null)
            {
                root = tail = handlerCtx;
            }
            else
            {

                DefaultPipelineHandlerContext pre = tail.NextHandlerContext;
                DefaultPipelineHandlerContext next = tail;
                tail.PreHandlerContext = handlerCtx;
                pre.NextHandlerContext = handlerCtx;
                handlerCtx.NextHandlerContext = next;
                handlerCtx.PreHandlerContext = pre;

                tail = handlerCtx;
            }
        }

        public void Remove(string name)
        {
            DefaultPipelineHandlerContext n = root;
            while (n != null)
            {
                if (n.HandlerName == name)
                {
                    break;
                }
                n = n.NextHandlerContext;
            }
            DoRemove(n);
        }

        private void DoRemove(DefaultPipelineHandlerContext n)
        {
            if (n == null)
            {
                throw new InvalidOperationException();
            }
            else if (n == tail && tail != root)
            {
                tail.PreHandlerContext.NextHandlerContext = null;
                tail = tail.PreHandlerContext;
            }
            else if (n == root && root != tail)
            {
                root.NextHandlerContext.PreHandlerContext = null;
                root = root.NextHandlerContext;
            }
            else
            {
                n.PreHandlerContext.NextHandlerContext = n.NextHandlerContext;
                n.NextHandlerContext.PreHandlerContext = n.PreHandlerContext;
            }
        }

        public void Remove(IPipelineHandler handler)
        {
            DefaultPipelineHandlerContext n = root;
            while (n != null)
            {
                if (n.Handler == handler)
                {
                    break;
                }
                n = n.NextHandlerContext;
            }
            DoRemove(n);
        }
    }
}