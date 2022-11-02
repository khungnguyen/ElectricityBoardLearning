Shader "Custom/MaskFrame"
{
    SubShader
    {
        Tags {"Queue" = "Geometry-1"}
        ColorMask 0
        ZWrite Off
        
        Pass {
            //testing
            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace
            } 
        }
    }

}
