using ErrorOr;
using MediatR;
using OutDinner.Application.Common.Interfaces.Persistence;
using OutDinner.Domain.Hosts.ValueObjects;
using OutDinner.Domain.Menus;
using OutDinner.Domain.Menus.Entities;

namespace OutDinner.Application.Menus.Commands.CreateMenu;

public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, ErrorOr<Menu>>
{
    private readonly IMenuRepository _menuRepository;

    public CreateMenuCommandHandler(IMenuRepository menuRepository) { _menuRepository = menuRepository; }

    public async Task<ErrorOr<Menu>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var menu = Menu.Create(
            hostId: HostId.Create(request.HostId),
            name: request.Name,
            description: request.Description,
            sections: request.Sections.ConvertAll(sections => MenuSection.Create(
                name: sections.Name,
                description: sections.Description,
                items: sections.Items.ConvertAll(items => MenuItem.Create(
                    name: items.Name,
                    description: items.Description)))));

        _menuRepository.Add(menu);

        return menu;
    }
}