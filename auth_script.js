const fs = require('fs');
const path = require('path');
const dir = path.join(__dirname, 'API', 'Controllers');
const files = fs.readdirSync(dir).filter(f => f.endsWith('.cs') && f !== 'AuthController.cs' && f !== 'ProductController.cs');

for(let f of files) {
  let content = fs.readFileSync(path.join(dir, f), 'utf8');
  if(!content.includes('[Authorize')) {
    let role = 'SuperAdmin';
    if(/Product|Supplier|Warehouse|Category/i.test(f)) role = 'ProductAdmin,SuperAdmin';
    else if(/Order|Shipping/i.test(f)) role = 'OrderAdmin,SuperAdmin';
    else if(/Customer/i.test(f)) role = 'CustomerAdmin,SuperAdmin';
    
    // special rules for ReportController later, but for now allow SuperAdmin and specific ones
    if(/Report/i.test(f)) role = 'SuperAdmin'; 
    
    content = content.replace('namespace', `using Microsoft.AspNetCore.Authorization;\n\nnamespace`);
    content = content.replace('[ApiController]', `[ApiController]\n    [Authorize(Roles = "${role}")]`);
    fs.writeFileSync(path.join(dir, f), content);
  }
}
