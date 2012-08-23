using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Atomic
{
    public class StateManager : IConsoleCommandable
    {
        public BufferedList<State> states { get; private set; }
        public List<State> focusState { get; private set; }

        bool toSort = false;
        bool paused = false;
        int sleep = 0;

        public StateManager()
        {
            states = new BufferedList<State>();
            focusState = new List<State>();
        }

        public void Update()
        {
            states.ApplyBuffers();
            if (toSort)
                states.Sort();
        }

        /// <summary>
        /// Adds the given state without adding it to the focus stack.
        /// </summary>
        public void AddState(State state)
        {
            states.AddBuffer.Add(state);
            toSort = true;
        }

        /// <summary>
        /// Adds the state and adds it to the top of the focus stack.
        /// </summary>
        public void StartState(State state)
        {
            states.AddBuffer.Add(state);
            AddFocus(state);
            toSort = true;
        }

        /// <summary>
        /// Removes the given state and removes all instances of it from the focus stack.
        /// </summary>
        /// <param name="state"></param>
        public void EndState(State state)
        {
            states.RemoveBuffer.Add(state);
            State cur = GetFocusedState();
            focusState.RemoveAll(i => i == state);
            if (GetFocusedState() != cur && GetFocusedState() != null)
            {
                state.OnBlur();
                GetFocusedState().OnFocus();
            }
            toSort = true;
        }

        /// <summary>
        /// Adds the given state to the top of the focus stack.
        /// </summary>
        public void AddFocus(State state)
        {
            if (focusState.Count > 0)
                GetFocusedState().OnBlur();
            focusState.Add(state);
            state.OnFocus();
        }

        /// <summary>
        /// Removes the state on the top of the focus stack to drop down to the next state.
        /// </summary>
        public void DropFocus()
        {
            GetFocusedState().OnBlur();
            focusState.RemoveAt(focusState.Count - 1);
            if (focusState.Count > 0)
                GetFocusedState().OnFocus();
        }

        /// <summary>
        /// Removes the given state from the focus stack.
        /// </summary>
        public void DropFocus(State state)
        {
            State cur = GetFocusedState();
            focusState.Remove(state);
            if (GetFocusedState() != cur && GetFocusedState() != null)
            {
                state.OnBlur();
                if (focusState.Count > 0)
                    GetFocusedState().OnFocus();
            }
        }

        /// <summary>
        /// Puts the given state on the top of the focus stack and removes all previous copies of the state from lower levels.
        /// If this state does not exist in the focus stack it will still be added to the top.
        /// </summary>
        public void BringToFront(State state)
        {
            State cur = GetFocusedState();
            focusState.RemoveAll(i => i == state);
            focusState.Add(state);
            if (GetFocusedState() != cur)
            {
                GetFocusedState().OnBlur();
                state.OnFocus();
            }
        }

        /// <summary>
        /// Returns the state currently on the top of the focus stack. If the stack is empty, null will be returned.
        /// </summary>
        public State GetFocusedState()
        {
            if (focusState.Count == 0)
                return null;
            return focusState[focusState.Count - 1];
        }

        /// <summary>
        /// Calls the appropriate update/backgroundUpdate function for each of the existing states.
        /// </summary>
        public void UpdateStates(GameTime gameTime)
        {
            foreach (State state in states)
            {
                if (state.enabled)
                {
                    if (state == GetFocusedState())
                        state.Update(gameTime);
                    else if (!paused)
                        state.BackgroundUpdate(gameTime);
                }
            }
        }

        /// <summary>
        /// Calls the appropriate draw/backgroundDraw function for each of the existing states.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch that the states should use to draw themselves.</param>
        public void DrawStates(SpriteBatch spriteBatch)
        {
            foreach (State state in states)
            {
                if (state.enabled)
                {
                    if (state == GetFocusedState())
                        state.Draw(spriteBatch);
                    else if (!paused)
                        state.BackgroundDraw(spriteBatch);
                }
            }
        }

        public void RegisterCommands(ConsoleState cs)
        {            
            cs.AddCommand("pause", delegate(ConsoleState cl, string[] args)
            {
                paused = args[0] == "1";
            },
            "Stops the State Manager from calling any Update or Draw functions on all background states when set to true."
            );

            cs.AddCommand("focus", delegate(ConsoleState cl, string[] args)
            {
                foreach (State state in states)
                {
                    if (state.GetType().Name == args[0])
                    {
                        focusState.Add(state);
                        return;
                    }
                }
                cl.AddLine("No state of type '" + args[0] + "' exist.");
            },
            "Adds the first state of type arg0 to the top of the focus stack."
            );

            cs.AddCommand("dropFocus", delegate(ConsoleState cl, string[] args)
            {
                if (GetFocusedState() != null)
                    DropFocus();
            },
            "Removes the currently focused state from the top of the focus stack."
            );

            cs.AddCommand("printFocusStack", delegate(ConsoleState cl, string[] args)
            {
                foreach (State state in focusState)
                {
                    cl.AddLine(state.GetType().Name, state == GetFocusedState() ? Color.Green : Color.White);
                }
            },
            "Prints the current focus stack to the console window."
            );

            cs.AddCommand("printStates", delegate(ConsoleState cl, string[] args)
            {
                foreach (State state in states)
                {
                    cl.AddLine(state.GetType().Name, state == GetFocusedState() ? Color.Green : Color.White);
                }
            },
            "Prints all currently running states to the console window."
            );
        }
    }
}
