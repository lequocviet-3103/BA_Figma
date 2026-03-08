import { useState } from 'react'
import './App.css'

function App() {

  const [prompt,setPrompt] = useState("")
  const [html,setHtml] = useState("")
  const [loading,setLoading] = useState(false)

 /* const generateUI = async () => {

    setLoading(true)
    setHtml("")

    try {

      const response = await fetch("https://www.1ui.dev/api/v1/generate",{
        method:"POST",
        headers:{
          "Content-Type":"application/json",
          "Authorization":"Bearer 1ui_sk_live_T-Tk_xj6KdZQe_RnaMfs8XMrTRd6Jbtl"
        },
        body:JSON.stringify({
          prompt:prompt,
          viewMode:"desktop"
        })
      })

      const text = await response.text()

      const lines = text.split("\n")

      let finalHtml = ""

      for(const line of lines){

        if(line.startsWith("data:")){

          const json = line.replace("data: ","")

          try{

            const obj = JSON.parse(json)

            if(obj.type === "done"){
              finalHtml = obj.code
            }

          }catch(e){
            console.error("Error parsing JSON:", e)
          }

        }

      }

      setHtml(finalHtml)

    } catch(err){

      console.error(err)

    }

    setLoading(false)

  }

  return (

    <div style={{
      padding:"40px",
      fontFamily:"Arial"
    }}>

      <h1>AI UI Generator</h1>

      <textarea
        style={{
          width:"600px",
          height:"120px",
          padding:"10px"
        }}
        placeholder="Describe the UI you want..."
        value={prompt}
        onChange={(e)=>setPrompt(e.target.value)}
      />

      <br/><br/>

      <button
        onClick={generateUI}
        style={{
          padding:"10px 20px",
          fontSize:"16px",
          cursor:"pointer"
        }}
      >
        {loading ? "Generating..." : "Generate UI"}
      </button>

      <hr style={{margin:"40px 0"}}/>

      <div
        style={{
          border:"1px solid #ddd",
          padding:"20px",
          borderRadius:"10px"
        }}
        dangerouslySetInnerHTML={{__html:html}}
      />

    </div>

  )
*/


const generateUI = async () => {

    setLoading(true)

    const response = await fetch("https://www.1ui.dev/api/v1/generate",{
      method:"POST",
      headers:{
        "Content-Type":"application/json",
        "Authorization":"Bearer 1ui_sk_live_T-Tk_xj6KdZQe_RnaMfs8XMrTRd6Jbtl"
      },
      body:JSON.stringify({
        prompt:prompt,
        viewMode:"desktop"
      })
    })

    const text = await response.text()

    const lines = text.split("\n")

    let htmlCode = ""

    lines.forEach(line => {

      if(line.startsWith("data:")){

        const jsonStr = line.replace("data: ","")

        try{

          const obj = JSON.parse(jsonStr)

          if(obj.type === "done"){
            htmlCode = obj.code
          }

        }catch(e){
          console.error("Error parsing JSON:", e)
        }
      }

    })

    setHtml(htmlCode)

    const json = convertHtmlToJson(htmlCode)

    sendToFigma(json)

    setLoading(false)

  }

  const convertHtmlToJson = (htmlString) => {

    const parser = new DOMParser()
    const doc = parser.parseFromString(htmlString,"text/html")

    const children = []

    doc.body.querySelectorAll("*").forEach(node => {

      if(node.tagName === "H1"){
        children.push({
          type:"text",
          value:node.textContent
        })
      }

      if(node.tagName === "BUTTON"){
        children.push({
          type:"button",
          value:node.textContent
        })
      }

    })

    return {children}

  }

  const sendToFigma = (json) => {

    window.parent.postMessage({
      pluginMessage:{
        type:"generate-ui",
        data:json
      }
    },"*")

  }

  return(

    <div style={{padding:40,fontFamily:"Arial"}}>

      <h1>AI → Figma UI Generator</h1>

      <textarea
        style={{width:600,height:120}}
        placeholder="Describe your UI..."
        value={prompt}
        onChange={(e)=>setPrompt(e.target.value)}
      />

      <br/><br/>

      <button onClick={generateUI}>
        {loading ? "Generating..." : "Generate UI"}
      </button>

      <hr/>

      <div
        dangerouslySetInnerHTML={{__html:html}}
      />

    </div>

  )


}

export default App