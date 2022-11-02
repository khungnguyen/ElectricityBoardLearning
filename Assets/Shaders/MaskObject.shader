Shader "Custom/MaskObject"
{
    
    SubShader
    {
       Pass {
        Stencil {
            Ref 1
            Comp Equal
        }
       }
    }
}
