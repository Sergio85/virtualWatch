    Shader "Transparent/transparentShadowCaster"
    {
    Subshader
    {
    UsePass "VertexLit/SHADOWCOLLECTOR"
    UsePass "VertexLit/SHADOWCASTER"
    }
     
    Fallback off
    }
