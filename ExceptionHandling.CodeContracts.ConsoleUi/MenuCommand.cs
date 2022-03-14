using System;
using System.Windows.Input;

namespace ExceptionHandling.CodeContracts.ConsoleUi
{
	/// <summary>
	/// Represents an action for menu option command.
	/// </summary>
	public class MenuCommand : ICommand
	{
		private readonly Func<object, bool> _canExecute;
		private readonly Action<object> _executeAction;

		/// <summary>
		/// Initializes a new instance of the <see cref="MenuCommand" /> class.
		/// </summary>
		/// <param name="executeAction">An action to execute.</param>
		public MenuCommand(Action<object> executeAction) : this(executeAction, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MenuCommand" /> class.
		/// </summary>
		/// <param name="executeAction">An action to execute.</param>
		/// <param name="canExecute">A func determines whether to perform an action or not.</param>
		public MenuCommand(Action<object> executeAction, Func<object, bool> canExecute)
		{
			_executeAction = executeAction;
			_canExecute = canExecute;
		}

		/// <summary>Defines the method that determines whether the command can execute in its current state.</summary>
		/// <returns>true if this command can be executed; otherwise, false.</returns>
		/// <param name="parameter">
		/// Data used by the command.  If the command does not require data to be passed, this object can
		/// be set to null.
		/// </param>
		public bool CanExecute(object parameter)
		{
			return _canExecute == null || _canExecute(parameter);
		}

		/// <summary>Defines the method to be called when the command is invoked.</summary>
		/// <param name="parameter">
		/// Data used by the command.  If the command does not require data to be passed, this object can
		/// be set to null.
		/// </param>
		public void Execute(object parameter)
		{
			if (!CanExecute(parameter))
			{
				return;
			}

			_executeAction(parameter);
		}

		/// <summary>Occurs when changes occur that affect whether or not the command should execute.</summary>
		public event EventHandler CanExecuteChanged;

		/// <summary>
		/// Invokes <see cref="CanExecuteChanged" />.
		/// </summary>
		public void OnCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}