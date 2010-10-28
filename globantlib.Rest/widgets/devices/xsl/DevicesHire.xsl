<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">


  <xsl:template match="/">
    <div id="w-devices-list">
      <xsl:for-each select="ArrayOfDeviceType/DeviceType">

        <div class="device_item">
          <div class="device_top">
            <span class="dispositive_attribute device_name">
              <xsl:value-of select="Type" />
            </span>
            <img src="/img/{Image}" alt="{Type}" class="device_thumbnail"/>
          </div>
          <div class="device_bottom">
            <ul class="device_description">
              <!--<li>
                <span class="dispositive_attribute">Type: </span>
                <xsl:value-of select="Type" />
              </li>-->
              <li>
                <span class="dispositive_attribute">Quantity: </span>
                <xsl:value-of select="Quantity" />
              </li>
              <!--<li>
                    <span class="dispositive_attribute">Availibility: </span>
                    <xsl:value-of select="available" />
                  </li>-->
            </ul>
            <div class="button_div">
              <a class="dispositive_button" href="#devices/calendar/{type}">
                <span>View</span>
                <span> Availibility</span>
              </a>
            </div>
          </div>


        </div>

      </xsl:for-each>
    </div>
  </xsl:template>
</xsl:stylesheet>
  