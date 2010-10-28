<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">


    <xsl:template match="/">
    
        <xsl:for-each select="ArrayOfDeviceType/DeviceType">
          <div id="w-devices-list">
            <div class="device_item">
              <img src="/img/{Image}" alt="{Type}" class="device_thumbnail"/>
              <ul class="device_description">
                <li>
                  <span class="dispositive_attribute">Type: </span> <xsl:value-of select="Type" />
                </li>
                <li>
                  <span class="dispositive_attribute">Quantity: </span> <xsl:value-of select="Quantity" />
                </li>
                <li>
                  <span class="dispositive_attribute">Disponibility: </span> <xsl:value-of select="Available" />
                </li>
              </ul>
              <a class="dispositive_button" href="#devices/calendar/{type}">View Disponibility</a>
            </div>
          </div>
        </xsl:for-each>
    
    </xsl:template>
</xsl:stylesheet>
