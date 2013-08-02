﻿// Copyright © 2013 Steelbreeze Limited.
// This file is part of state.cs.
//
// state.cs is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published
// by the Free Software Foundation, either version 3 of the License,
// or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Steelbreeze.Behavior
{
	/// <summary>
	/// A Vertex is a node within a state machine model that can be the source or target of a transition.
	/// </summary>
	public abstract class Vertex : StateMachineElement
	{
		/// <summary>
		/// Returns the parent element of this element
		/// </summary>
		public Region Parent { get; private set; }

		internal Vertex( Region parent )
		{
			if( ( this.Parent = parent ) != null )
				parent.vertices.Add( this );
		}

		internal IEnumerable<StateMachineElement> Ancestors
		{
			get
			{
				for( var element = this; element != null; element = element.Parent.Parent )
				{
					yield return element;

					if( element.Parent == null )
						break;

					yield return element.Parent;
				}
			}
		}

		abstract internal void Complete( IState state, Boolean deepHistory );
	}
}