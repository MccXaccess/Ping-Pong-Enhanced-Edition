using Godot;

namespace PingPong_Extensions
{
    /// <summary>
    /// Extensions i made because of unsupported default casting in godot.
    /// Extensions by: MccXaccess
    /// </summary>
    public static class NodeExtensions
    {
        /// <summary>
        /// Either use these Extensions with namespace or static.
        /// Examples with Namespace:
        /// using PingPong_Extensions;
        /// someVariable.AsNode2D(); 
        ///           
        /// Examples with Static:
        /// NodeExtensions.AsNode2D(someVariable);
        /// 
        /// Both of these cases are valid and can be stored into a variable
        /// or directly access their properties like GlobalPosition.
        /// </summary>
        /// <param name="node">Value to convert</param>
        /// <returns>Converted value from 'Node' to 'Node2D'</returns>
        public static Node2D AsNode2D(this Node node)
        {
            return node as Node2D;
        }

        public static Node3D AsNode3D(this Node node)
        {
            return node as Node3D;
        }
    }
}