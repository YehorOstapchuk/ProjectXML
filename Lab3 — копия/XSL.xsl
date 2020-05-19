<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

    <xsl:output method="html"></xsl:output>
  
    <xsl:template match="/">
    <html>
      <body>
      <table border="1">

        <TR bgcolor="#008000">
          <TD><strong>Name</strong></TD>
          <TD><strong>Faculty</strong></TD>
          <TD><strong>Department</strong></TD> 
          <TD><strong>Position</strong></TD>
          <TD><strong>Pension</strong></TD>
          <TD><strong>Experiense</strong></TD>
        </TR>

        <xsl:for-each select="Teachers/Teacher">      
        <TR>
          <TD><b><xsl:value-of select="@Name"/></b></TD>
          <TD><xsl:value-of select="@Faculty"/></TD>
          <TD><xsl:value-of select="@Department"/></TD>
          <TD><xsl:value-of select="@Position"/></TD>
          <TD><xsl:value-of select="@Pension"/></TD>
          <TD><xsl:value-of select="@Experiense"/></TD>
        </TR>
        </xsl:for-each>
 
      </table>
  </body>
 </html>

  
    </xsl:template>
  
</xsl:stylesheet>