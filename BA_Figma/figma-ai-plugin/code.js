figma.showUI(__html__,{width:900,height:900})

figma.ui.onmessage = async msg=>{

  if(msg.type==="generate-ui"){
    const frame = figma.createFrame()

   figma.currentPage.appendChild(frame)

    const buildNode = async (node,parent)=>{

      if(node.type==="text"){

        await figma.loadFontAsync({family:"Inter",style:"Regular"})

        const t=figma.createText()

        t.characters=node.value
        t.fontSize=node.fontSize || 16

        parent.appendChild(t)

      }

      if(node.type==="button"){

        const rect=figma.createRectangle()

        rect.resize(node.width,node.height)

        await figma.loadFontAsync({family:"Inter",style:"Regular"})

        const label=figma.createText()

        label.characters=node.value

        parent.appendChild(rect)
        parent.appendChild(label)

      }

      if(node.type==="input"){

        const r=figma.createRectangle()

        r.resize(240,40)

        parent.appendChild(r)

      }

      if(node.type==="frame"){

        const frame=figma.createFrame()

        frame.layoutMode="VERTICAL"
        frame.itemSpacing=16
        frame.paddingTop=20
        frame.paddingBottom=20
        frame.paddingLeft=20
        frame.paddingRight=20

        parent.appendChild(frame)

        for(const child of node.children){

          await buildNode(child,frame)

        }

      }

    }

    const root=figma.createFrame()

    root.layoutMode="VERTICAL"

    figma.currentPage.appendChild(root)

    await buildNode(msg.data,root)

    figma.viewport.scrollAndZoomIntoView([root])

  }

}